using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaBox.Storing.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "EntityId",
                keyValue: 1L,
                column: "Name",
                value: "Papajohn");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "EntityId",
                keyValue: 2L,
                column: "Name",
                value: "Papamurphy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "EntityId",
                keyValue: 1L,
                column: "Name",
                value: "Chitown Main Street");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "EntityId",
                keyValue: 2L,
                column: "Name",
                value: "Big Apple");
        }
    }
}
