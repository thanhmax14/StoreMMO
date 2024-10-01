using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreMMO.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatev6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "quantity",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "Carts");
        }
    }
}
