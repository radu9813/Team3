using Microsoft.EntityFrameworkCore.Migrations;

namespace Team3Assig.Migrations
{
    public partial class Diplomas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Diploma",
                columns: new[] { "DiplomaId", "Abstract", "Completeness", "Supervisor", "Thesis" },
                values: new object[] { 1, "Big description of the Thesis", true, "Borys", "Computer Science" });

            migrationBuilder.InsertData(
                table: "Diploma",
                columns: new[] { "DiplomaId", "Abstract", "Completeness", "Supervisor", "Thesis" },
                values: new object[] { 2, "Big description of the Thesis", true, "Borys", "Computer Engineering" });

            migrationBuilder.InsertData(
                table: "Diploma",
                columns: new[] { "DiplomaId", "Abstract", "Completeness", "Supervisor", "Thesis" },
                values: new object[] { 3, "Big description of the Thesis", true, "Borys", "Computer Architecture" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Diploma",
                keyColumn: "DiplomaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Diploma",
                keyColumn: "DiplomaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Diploma",
                keyColumn: "DiplomaId",
                keyValue: 3);
        }
    }
}
