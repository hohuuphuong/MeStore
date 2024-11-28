using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class mofidyProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Product_Categories_ProductCategory_Id",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategory_Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Product_Categories_ProductCategory_Id",
                table: "Products",
                column: "ProductCategory_Id",
                principalTable: "Product_Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Product_Categories_ProductCategory_Id",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategory_Id",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Product_Categories_ProductCategory_Id",
                table: "Products",
                column: "ProductCategory_Id",
                principalTable: "Product_Categories",
                principalColumn: "Id");
        }
    }
}
