using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Carts_Cart_Id",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Detail_Order_Oder_Id",
                table: "Order_Detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Detail_Payment_Method_PaymentMethod_Id",
                table: "Order_Detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment_Method",
                table: "Payment_Method");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order_Detail",
                table: "Order_Detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Payment_Method",
                newName: "Payment_Methods");

            migrationBuilder.RenameTable(
                name: "Order_Detail",
                newName: "Order_Details");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Detail_PaymentMethod_Id",
                table: "Order_Details",
                newName: "IX_Order_Details_PaymentMethod_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Cart_Id",
                table: "Orders",
                newName: "IX_Orders_Cart_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment_Methods",
                table: "Payment_Methods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order_Details",
                table: "Order_Details",
                column: "Oder_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Orders_Oder_Id",
                table: "Order_Details",
                column: "Oder_Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Payment_Methods_PaymentMethod_Id",
                table: "Order_Details",
                column: "PaymentMethod_Id",
                principalTable: "Payment_Methods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Carts_Cart_Id",
                table: "Orders",
                column: "Cart_Id",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Orders_Oder_Id",
                table: "Order_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Payment_Methods_PaymentMethod_Id",
                table: "Order_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Carts_Cart_Id",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment_Methods",
                table: "Payment_Methods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order_Details",
                table: "Order_Details");

            migrationBuilder.RenameTable(
                name: "Payment_Methods",
                newName: "Payment_Method");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Order_Details",
                newName: "Order_Detail");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Cart_Id",
                table: "Order",
                newName: "IX_Order_Cart_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Details_PaymentMethod_Id",
                table: "Order_Detail",
                newName: "IX_Order_Detail_PaymentMethod_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment_Method",
                table: "Payment_Method",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order_Detail",
                table: "Order_Detail",
                column: "Oder_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Carts_Cart_Id",
                table: "Order",
                column: "Cart_Id",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Detail_Order_Oder_Id",
                table: "Order_Detail",
                column: "Oder_Id",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Detail_Payment_Method_PaymentMethod_Id",
                table: "Order_Detail",
                column: "PaymentMethod_Id",
                principalTable: "Payment_Method",
                principalColumn: "Id");
        }
    }
}
