using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantServer.Models
{
    public class RestaurantModel:DbContext
    {
        public RestaurantModel(DbContextOptions<RestaurantModel> db):base(db)
        {

        }
       public DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
    }

    public partial class Item
    {

        public Item()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey("Category")]
        public int CatId { get; set; }
        public virtual Category Category{get;set;}
       
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
    public partial class OrderItem
    {
        public int ID { get; set; }
        public int OrderId { get; set; }
        public long PrdID { get; set; }
        public int  Qty { get; set; }
        public int Price { get; set; }
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
    public partial class Order
    {
      
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        public int OrderID { get; set; }
        [ForeignKey("Customer")]
        public int CusID { get; set; }
        //public string PMethod { get; set; }
        //public float GTotal { get; set; }
        public string OrderNo { get; set; }
        public float TotalPrice { get; set; }
        public string Paymethod { get; set; }
        public virtual Customer Customer { get; set; }
        [NotMapped]
        public string DeletedOrderItemIDs { get; set; }
        
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        
        
    }
    public partial class Customer
    {
        
        public Customer()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        
        public virtual ICollection<Order> Orders { get; set; }
    }
    public partial class Category
    {
        
        public Category()
        {
            this.Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ParentID { get; set; }
  
        public virtual ICollection<Item> Items { get; set; }

    }
}
