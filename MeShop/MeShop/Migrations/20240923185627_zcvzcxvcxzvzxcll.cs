using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class zcvzcxvcxzvzxcll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "Blog",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "Blog");
        }
    }
}
