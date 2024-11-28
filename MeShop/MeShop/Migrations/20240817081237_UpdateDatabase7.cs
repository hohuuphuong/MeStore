using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cart_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Carts_Cart_Id",
                        column: x => x.Cart_Id,
                        principalTable: "Carts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payment_Method",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment_Method", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Detail",
                columns: table => new
                {
                    Oder_Id = table.Column<int>(type: "int", nullable: false),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Oder_Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentMethod_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Detail", x => x.Oder_Id);
                    table.ForeignKey(
                        name: "FK_Order_Detail_Order_Oder_Id",
                        column: x => x.Oder_Id,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Detail_Payment_Method_PaymentMethod_Id",
                        column: x => x.PaymentMethod_Id,
                        principalTable: "Payment_Method",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Cart_Id",
                table: "Order",
                column: "Cart_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Detail_PaymentMethod_Id",
                table: "Order_Detail",
                column: "PaymentMethod_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order_Detail");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Payment_Method");
        }
    }
}
