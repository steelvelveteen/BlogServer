using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAPI.Data.Migrations
{
    public partial class createProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestModels");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 901, "Vorkwerk Thermomix", 3500m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.CreateTable(
                name: "TestModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestModelName = table.Column<string>(type: "TEXT", nullable: false),
                    TestModelOtherProperty = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestModels", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TestModels",
                columns: new[] { "Id", "TestModelName", "TestModelOtherProperty" },
                values: new object[] { 10, "First name", "Some other property name" });

            migrationBuilder.InsertData(
                table: "TestModels",
                columns: new[] { "Id", "TestModelName", "TestModelOtherProperty" },
                values: new object[] { 20, "Sec name", "Some sec property name" });

            migrationBuilder.InsertData(
                table: "TestModels",
                columns: new[] { "Id", "TestModelName", "TestModelOtherProperty" },
                values: new object[] { 30, "Tres name", "Some tres property name" });
        }
    }
}
