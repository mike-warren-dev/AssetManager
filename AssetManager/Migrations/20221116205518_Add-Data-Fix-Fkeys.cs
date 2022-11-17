using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetManager.Migrations
{
    /// <inheritdoc />
    public partial class AddDataFixFkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Order_OrderId",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrder_OrderId",
                table: "ProductOrder");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductOrder",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "ProductOrder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder",
                columns: new[] { "OrderId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Order_ApproverId",
                table: "Order",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PurchaserId",
                table: "Order",
                column: "PurchaserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Person_ApproverId",
                table: "Order",
                column: "ApproverId",
                principalTable: "Person",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Person_PurchaserId",
                table: "Order",
                column: "PurchaserId",
                principalTable: "Person",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Order_OrderId",
                table: "ProductOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Person_ApproverId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Person_PurchaserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Order_OrderId",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_Order_ApproverId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_PurchaserId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductOrder",
                newName: "ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "ProductOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_OrderId",
                table: "ProductOrder",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Order_OrderId",
                table: "ProductOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId");
        }
    }
}
