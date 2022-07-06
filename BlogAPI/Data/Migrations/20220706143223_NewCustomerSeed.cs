using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAPI.Data.Migrations
{
    public partial class NewCustomerSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "FirstName", "LastName", "Phone" },
                values: new object[] { 66777, null, "Scarlett", "Amancia", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 66777);
        }
    }
}
