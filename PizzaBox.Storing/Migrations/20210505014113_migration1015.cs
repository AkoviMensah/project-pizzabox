using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaBox.Storing.Migrations
{
    public partial class migration1015 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "EntityId",
                keyValue: 5L);

            migrationBuilder.UpdateData(
                table: "Crust",
                keyColumn: "EntityId",
                keyValue: 1L,
                column: "Price",
                value: 4.00m);

            migrationBuilder.UpdateData(
                table: "Crust",
                keyColumn: "EntityId",
                keyValue: 2L,
                column: "Price",
                value: 4.00m);

            migrationBuilder.UpdateData(
                table: "Crust",
                keyColumn: "EntityId",
                keyValue: 3L,
                column: "Price",
                value: 4.00m);

            migrationBuilder.UpdateData(
                table: "Crust",
                keyColumn: "EntityId",
                keyValue: 4L,
                column: "Price",
                value: 4.00m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Crust",
                keyColumn: "EntityId",
                keyValue: 1L,
                column: "Price",
                value: 3.00m);

            migrationBuilder.UpdateData(
                table: "Crust",
                keyColumn: "EntityId",
                keyValue: 2L,
                column: "Price",
                value: 3.00m);

            migrationBuilder.UpdateData(
                table: "Crust",
                keyColumn: "EntityId",
                keyValue: 3L,
                column: "Price",
                value: 3.00m);

            migrationBuilder.UpdateData(
                table: "Crust",
                keyColumn: "EntityId",
                keyValue: 4L,
                column: "Price",
                value: 3.00m);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "EntityId", "Name" },
                values: new object[] { 5L, "Abalo" });
        }
    }
}
