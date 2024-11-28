using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class UpdataDatabase6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Size_ProductInventory_Id",
                table: "Products_Size");

            migrationBuilder.AlterColumn<int>(
                name: "ProductInventory_Id",
                table: "Products_Size",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Size_ProductInventory_Id",
                table: "Products_Size",
                column: "ProductInventory_Id",
                unique: true,
                filter: "[ProductInventory_Id] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Size_ProductInventory_Id",
                table: "Products_Size");

            migrationBuilder.AlterColumn<int>(
                name: "ProductInventory_Id",
                table: "Products_Size",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Size_ProductInventory_Id",
                table: "Products_Size",
                column: "ProductInventory_Id",
                unique: true);
        }
    }
}
