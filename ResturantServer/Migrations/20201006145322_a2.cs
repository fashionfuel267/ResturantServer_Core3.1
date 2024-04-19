using Microsoft.EntityFrameworkCore.Migrations;

namespace ResturantServer.Migrations
{
    public partial class a2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentID" },
                values: new object[,]
                {
                    { 1, null, "Electronics", 0 },
                    { 2, null, "Cosmetics", 0 },
                    { 3, null, "Crokeries", 0 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kamal" },
                    { 2, "Jamal" },
                    { 3, "Alom" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CatId", "ImagePath", "Name", "Price" },
                values: new object[] { 1, 1, null, "Bulb", 0f });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CatId", "ImagePath", "Name", "Price" },
                values: new object[] { 2, 1, null, "Mouse", 0f });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CatId", "ImagePath", "Name", "Price" },
                values: new object[] { 3, 1, null, "Keyboard", 0f });
        }
    }
}
