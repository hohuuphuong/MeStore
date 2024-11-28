using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_Product_Id",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Product_Categories_ProductCategory_Id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Addresses_Users_User_Id",
                table: "User_Addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_Product_Id",
                table: "Images",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Product_Categories_ProductCategory_Id",
                table: "Products",
                column: "ProductCategory_Id",
                principalTable: "Product_Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Addresses_Users_User_Id",
                table: "User_Addresses",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_Product_Id",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Product_Categories_ProductCategory_Id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Addresses_Users_User_Id",
                table: "User_Addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_Product_Id",
                table: "Images",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Product_Categories_ProductCategory_Id",
                table: "Products",
                column: "ProductCategory_Id",
                principalTable: "Product_Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Addresses_Users_User_Id",
                table: "User_Addresses",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
