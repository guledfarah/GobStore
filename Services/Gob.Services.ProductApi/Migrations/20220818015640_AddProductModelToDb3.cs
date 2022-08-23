using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gob.Services.ProductApi.Migrations
{
    public partial class AddProductModelToDb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "Products",
                newName: "ImageUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "ImageUrls");
        }
    }
}
