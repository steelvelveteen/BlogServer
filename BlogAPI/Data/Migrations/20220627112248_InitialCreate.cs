using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAPI.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestModels");
        }
    }
}
