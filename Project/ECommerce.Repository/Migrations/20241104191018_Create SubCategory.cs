using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreateSubCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubCategory",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategory",
                table: "Products");
        }
    }
}
