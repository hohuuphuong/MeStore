using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeShop.Migrations
{
    /// <inheritdoc />
    public partial class dvnmcxzmvncxzvhu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Images_Image_Id",
                table: "Blog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "Blogs");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_Image_Id",
                table: "Blogs",
                newName: "IX_Blogs_Image_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Images_Image_Id",
                table: "Blogs",
                column: "Image_Id",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Images_Image_Id",
                table: "Blogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blog");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_Image_Id",
                table: "Blog",
                newName: "IX_Blog_Image_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Images_Image_Id",
                table: "Blog",
                column: "Image_Id",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
