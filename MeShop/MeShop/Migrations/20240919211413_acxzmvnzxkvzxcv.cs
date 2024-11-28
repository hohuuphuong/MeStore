using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class acxzmvnzxkvzxcv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_Email",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "User_Phone",
                table: "Orders",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "User_Name",
                table: "Orders",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Orders",
                newName: "User_Phone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Orders",
                newName: "User_Name");

            migrationBuilder.AddColumn<string>(
                name: "User_Email",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
