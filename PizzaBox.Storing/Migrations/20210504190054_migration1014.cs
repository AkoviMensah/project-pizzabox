using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaBox.Storing.Migrations
{
    public partial class migration1014 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Pizzas_PizzaEntityId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_PizzaEntityId",
                table: "Order");

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "EntityId",
                keyValue: 4L);

            migrationBuilder.DropColumn(
                name: "PizzaEntityId",
                table: "Order");

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

            migrationBuilder.UpdateData(
                table: "Sizes",
                keyColumn: "EntityId",
                keyValue: 1L,
                column: "Price",
                value: 6.00m);

            migrationBuilder.UpdateData(
                table: "Sizes",
                keyColumn: "EntityId",
                keyValue: 2L,
                column: "Price",
                value: 8.00m);

            migrationBuilder.UpdateData(
                table: "Sizes",
                keyColumn: "EntityId",
                keyValue: 3L,
                column: "Price",
                value: 10.00m);

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "EntityId",
                keyValue: 1L,
                column: "Price",
                value: 1.00m);

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "EntityId",
                keyValue: 2L,
                column: "Price",
                value: 1.00m);

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "EntityId",
                keyValue: 3L,
                column: "Price",
                value: 1.00m);

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "EntityId",
                keyValue: 4L,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Pepperoni", 1.00m });

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "EntityId",
                keyValue: 5L,
                column: "Price",
                value: 1.00m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PizzaEntityId",
                table: "Order",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Crust",
                keyColumn: "EntityId",
                keyValue: 1L,
                column: "Price",
                value: 1.00m);

            migrationBuilder.UpdateData(
                table: "Crust",
                keyColumn: "EntityId",
                keyValue: 2L,
                column: "Price",
                value: 1.00m);

            migrationBuilder.UpdateData(
                table: "Crust",
                keyColumn: "EntityId",
                keyValue: 3L,
                column: "Price",
                value: 1.00m);

            migrationBuilder.UpdateData(
                table: "Crust",
                keyColumn: "EntityId",
                keyValue: 4L,
                column: "Price",
                value: 1.00m);

            migrationBuilder.UpdateData(
                table: "Sizes",
                keyColumn: "EntityId",
                keyValue: 1L,
                column: "Price",
                value: 10.00m);

            migrationBuilder.UpdateData(
                table: "Sizes",
                keyColumn: "EntityId",
                keyValue: 2L,
                column: "Price",
                value: 12.00m);

            migrationBuilder.UpdateData(
                table: "Sizes",
                keyColumn: "EntityId",
                keyValue: 3L,
                column: "Price",
                value: 15.00m);

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "EntityId", "Name", "Price" },
                values: new object[] { 4L, "Eating Challange(XXXL)", 250.00m });

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "EntityId",
                keyValue: 1L,
                column: "Price",
                value: 0.25m);

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "EntityId",
                keyValue: 2L,
                column: "Price",
                value: 0.25m);

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "EntityId",
                keyValue: 3L,
                column: "Price",
                value: 0.25m);

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "EntityId",
                keyValue: 4L,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Mozzarella", 0.25m });

            migrationBuilder.UpdateData(
                table: "Toppings",
                keyColumn: "EntityId",
                keyValue: 5L,
                column: "Price",
                value: 0.25m);

            migrationBuilder.CreateIndex(
                name: "IX_Order_PizzaEntityId",
                table: "Order",
                column: "PizzaEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Pizzas_PizzaEntityId",
                table: "Order",
                column: "PizzaEntityId",
                principalTable: "Pizzas",
                principalColumn: "EntityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
