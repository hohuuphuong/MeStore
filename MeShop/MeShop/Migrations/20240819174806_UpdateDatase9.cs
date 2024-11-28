using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatase9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Items_Products_Product_Id",
                table: "Cart_Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Size_Sizes_Size_Id",
                table: "Products_Size");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products_Size",
                table: "Products_Size");

            migrationBuilder.RenameColumn(
                name: "Product_Id",
                table: "Cart_Items",
                newName: "ProductSize_Id");

            migrationBuilder.AlterColumn<int>(
                name: "Product_Id",
                table: "Products_Size",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Size_Id",
                table: "Products_Size",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Products_Size",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products_Size",
                table: "Products_Size",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Size_Size_Id_Product_Id",
                table: "Products_Size",
                columns: new[] { "Size_Id", "Product_Id" },
                unique: true,
                filter: "[Size_Id] IS NOT NULL AND [Product_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Items_Products_Size_ProductSize_Id",
                table: "Cart_Items",
                column: "ProductSize_Id",
                principalTable: "Products_Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Size_Sizes_Size_Id",
                table: "Products_Size",
                column: "Size_Id",
                principalTable: "Sizes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Items_Products_Size_ProductSize_Id",
                table: "Cart_Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Size_Sizes_Size_Id",
                table: "Products_Size");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products_Size",
                table: "Products_Size");

            migrationBuilder.DropIndex(
                name: "IX_Products_Size_Size_Id_Product_Id",
                table: "Products_Size");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Products_Size");

            migrationBuilder.RenameColumn(
                name: "ProductSize_Id",
                table: "Cart_Items",
                newName: "Product_Id");

            migrationBuilder.AlterColumn<int>(
                name: "Size_Id",
                table: "Products_Size",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Product_Id",
                table: "Products_Size",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products_Size",
                table: "Products_Size",
                columns: new[] { "Size_Id", "Product_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Items_Products_Product_Id",
                table: "Cart_Items",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Size_Sizes_Size_Id",
                table: "Products_Size",
                column: "Size_Id",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
