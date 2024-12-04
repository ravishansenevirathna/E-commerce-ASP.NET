using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateOneToManyNewN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryCode",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryCode",
                table: "Categories");
        }
    }
}
