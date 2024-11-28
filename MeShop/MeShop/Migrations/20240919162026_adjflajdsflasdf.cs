using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class adjflajdsflasdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payment_Methods_PaymentMethod_Id",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod_Id",
                table: "Orders",
                newName: "Payment_MethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PaymentMethod_Id",
                table: "Orders",
                newName: "IX_Orders_Payment_MethodId");

            migrationBuilder.AddColumn<string>(
                name: "Payment_Method",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payment_Methods_Payment_MethodId",
                table: "Orders",
                column: "Payment_MethodId",
                principalTable: "Payment_Methods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payment_Methods_Payment_MethodId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Payment_Method",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Payment_MethodId",
                table: "Orders",
                newName: "PaymentMethod_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Payment_MethodId",
                table: "Orders",
                newName: "IX_Orders_PaymentMethod_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payment_Methods_PaymentMethod_Id",
                table: "Orders",
                column: "PaymentMethod_Id",
                principalTable: "Payment_Methods",
                principalColumn: "Id");
        }
    }
}
