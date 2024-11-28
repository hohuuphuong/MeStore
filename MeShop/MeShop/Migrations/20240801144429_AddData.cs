using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers");

            migrationBuilder.RenameTable(
                name: "AdminUsers",
                newName: "Admin_Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin_Users",
                table: "Admin_Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount_Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products_Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products_Inventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cart_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Carts_Cart_Id",
                        column: x => x.Cart_Id,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductCategory_Id = table.Column<int>(type: "int", nullable: true),
                    Discount_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Discounts_Discount_Id",
                        column: x => x.Discount_Id,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Product_Categories_ProductCategory_Id",
                        column: x => x.ProductCategory_Id,
                        principalTable: "Product_Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User_Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Addresses_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cart_Items",
                columns: table => new
                {
                    Cart_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    IsSelection = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart_Items", x => new { x.Product_Id, x.Cart_Id });
                    table.ForeignKey(
                        name: "FK_Cart_Items_Carts_Cart_Id",
                        column: x => x.Cart_Id,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cart_Items_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products_Size",
                columns: table => new
                {
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Size_Id = table.Column<int>(type: "int", nullable: false),
                    ProductInventory_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products_Size", x => new { x.Size_Id, x.Product_Id });
                    table.ForeignKey(
                        name: "FK_Products_Size_Products_Inventory_ProductInventory_Id",
                        column: x => x.ProductInventory_Id,
                        principalTable: "Products_Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Size_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Size_Sizes_Size_Id",
                        column: x => x.Size_Id,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Items_Cart_Id",
                table: "Cart_Items",
                column: "Cart_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Product_Id",
                table: "Images",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Discount_Id",
                table: "Products",
                column: "Discount_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategory_Id",
                table: "Products",
                column: "ProductCategory_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Size_Product_Id",
                table: "Products_Size",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Size_ProductInventory_Id",
                table: "Products_Size",
                column: "ProductInventory_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Addresses_User_Id",
                table: "User_Addresses",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Cart_Id",
                table: "Users",
                column: "Cart_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart_Items");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Products_Size");

            migrationBuilder.DropTable(
                name: "User_Addresses");

            migrationBuilder.DropTable(
                name: "Products_Inventory");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Product_Categories");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin_Users",
                table: "Admin_Users");

            migrationBuilder.RenameTable(
                name: "Admin_Users",
                newName: "AdminUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers",
                column: "Id");
        }
    }
}
