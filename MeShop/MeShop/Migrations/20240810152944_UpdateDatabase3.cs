using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_User_Id",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_User_Id",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Carts");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Cart_Id",
                table: "Users",
                column: "Cart_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Carts_Cart_Id",
                table: "Users",
                column: "Cart_Id",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Carts_Cart_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Cart_Id",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_User_Id",
                table: "Carts",
                column: "User_Id",
                unique: true,
                filter: "[User_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_User_Id",
                table: "Carts",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
