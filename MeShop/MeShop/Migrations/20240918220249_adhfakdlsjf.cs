using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class adhfakdlsjf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "PK_Order_Details",
                table: "Order_Details");

            migrationBuilder.DropIndex(
                name: "IX_Order_Details_PaymentMethod_Id",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "Address_Detail",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "Address_District",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "Address_Ward",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "Modified_At",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "Oder_Note",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "PaymentMethod_Id",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "User_Email",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "User_Name",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "User_Phone",
                table: "Order_Details");

            migrationBuilder.RenameColumn(
                name: "Cart_Id",
                table: "Orders",
                newName: "User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Cart_Id",
                table: "Orders",
                newName: "IX_Orders_User_Id");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Order_Details",
                newName: "Unit_Price");

            migrationBuilder.RenameColumn(
                name: "Oder_Id",
                table: "Order_Details",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Detail",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_District",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Ward",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified_At",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Oder_Note",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod_Id",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "User_Email",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User_Name",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User_Phone",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Order_Details",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Discount_Percent",
                table: "Order_Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order_Id",
                table: "Order_Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Product_Size_Id",
                table: "Order_Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order_Details",
                table: "Order_Details",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethod_Id",
                table: "Orders",
                column: "PaymentMethod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Details_Order_Id",
                table: "Order_Details",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Details_Product_Size_Id",
                table: "Order_Details",
                column: "Product_Size_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Orders_Order_Id",
                table: "Order_Details",
                column: "Order_Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Products_Size_Product_Size_Id",
                table: "Order_Details",
                column: "Product_Size_Id",
                principalTable: "Products_Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payment_Methods_PaymentMethod_Id",
                table: "Orders",
                column: "PaymentMethod_Id",
                principalTable: "Payment_Methods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_User_Id",
                table: "Orders",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Orders_Order_Id",
                table: "Order_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Products_Size_Product_Size_Id",
                table: "Order_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payment_Methods_PaymentMethod_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_User_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentMethod_Id",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order_Details",
                table: "Order_Details");

            migrationBuilder.DropIndex(
                name: "IX_Order_Details_Order_Id",
                table: "Order_Details");

            migrationBuilder.DropIndex(
                name: "IX_Order_Details_Product_Size_Id",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Address_Detail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Address_District",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Address_Ward",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Modified_At",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Oder_Note",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethod_Id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "User_Email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "User_Name",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "User_Phone",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "Discount_Percent",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "Order_Id",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "Product_Size_Id",
                table: "Order_Details");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Orders",
                newName: "Cart_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_User_Id",
                table: "Orders",
                newName: "IX_Orders_Cart_Id");

            migrationBuilder.RenameColumn(
                name: "Unit_Price",
                table: "Order_Details",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Order_Details",
                newName: "Oder_Id");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Order_Details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Detail",
                table: "Order_Details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_District",
                table: "Order_Details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Ward",
                table: "Order_Details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "Order_Details",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified_At",
                table: "Order_Details",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Oder_Note",
                table: "Order_Details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod_Id",
                table: "Order_Details",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_Email",
                table: "Order_Details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User_Name",
                table: "Order_Details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User_Phone",
                table: "Order_Details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order_Details",
                table: "Order_Details",
                column: "Oder_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Details_PaymentMethod_Id",
                table: "Order_Details",
                column: "PaymentMethod_Id");

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
    }
}
