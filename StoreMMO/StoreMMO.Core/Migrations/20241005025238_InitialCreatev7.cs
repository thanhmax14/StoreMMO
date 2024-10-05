using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreMMO.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatev7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDelete",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRestore",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDelete",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateRestore",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Users");
        }
    }
}
