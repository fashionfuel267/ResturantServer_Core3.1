using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ResturantServer.Models;

namespace ResturantServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly RestaurantModel _context;

        public OrdersController(RestaurantModel context)
        {
            _context = context;
        }
        [HttpGet("AllOrder")]
        public IActionResult GetOrders()
        {
            var result = (from o in _context.Orders
                          join c in _context.Customers on o.CusID equals c.Id
                          select new
                          {
                              o.OrderID,
                              o.OrderNo,
                              o.TotalPrice,
                              o.Paymethod,
                              Customer = c.Name,
                              DeletedOrderItemIDs = ""
                          }).ToList();

            return Ok(result);
        }
        // GET: api/Orders/5
        [HttpGet("orderbyID/{id}")]
        public IActionResult GetOrder(int id)
        {
            //Order order = db.Orders.Find(id);
            //if (order == null)
            //{
            //    return NotFound();
            //}

            var order = (from o in _context.Orders
                         join c in _context.Customers on o.CusID equals c.Id
                         where o.OrderID.Equals(id)
                         select new
                         {

                             o.OrderID,
                             o.OrderNo,
                             o.TotalPrice,
                             o.Paymethod,
                             o.CusID,
                             Customer =new  {Name= c.Name,Id=c.Id}
                         }).FirstOrDefault();

            var orderDetails = (from d in _context.OrderItems
                                join i in _context.Items on d.PrdID equals i.Id
                                where d.OrderId == id
                                select new
                                {
                                    d.ID,
                                    d.OrderId,
                                    d.Qty,
                                    d.PrdID,
                                    ItemName = i.Name,
                                    i.Price,
                                    Total = d.Qty * i.Price
                                }).ToList();
            return Ok(new { order, orderDetails });

        }
        // GET: api/Orders
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        //{
        //    return await _context.Orders.ToListAsync();
        //}

        //// GET: api/Orders/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Order>> GetOrder(int id)
        //{
        //    var order = await _context.Orders.FindAsync(id);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return order;
        //}

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Order>> PostOrder(Order order)
        //{
        //    _context.Orders.Add(order);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
        //}

        // POST: api/Orders
       
        [Route("Orderposted")]
        public IActionResult PostOrder(Order order)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {


                try
                {
                    if (order.OrderID == 0)
                        _context.Orders.Add(order);
                    else
                        _context.Entry(order).State = EntityState.Modified;
                    //_context.SaveChanges();

                    foreach (var item in order.OrderItems)
                    {
                        if (item.ID == 0)
                            _context.OrderItems.Add(item);
                        else
                            _context.Entry(item).State = EntityState.Modified;
                    }
                    if (order.DeletedOrderItemIDs != null)
                    {


                        foreach (var id in order.DeletedOrderItemIDs.Split(',').Where(x => x != ""))
                        {
                            OrderItem i = _context.OrderItems.Find(Int32.Parse(id));
                            _context.OrderItems.Remove( i);
                        }
                    }
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    _context.RemoveRange(order);
                    throw ex;
                }
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }
}
