using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class userheadermenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Menu10",
                table: "Header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Menu10Link",
                table: "Header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Menu10Status",
                table: "Header",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Menu11",
                table: "Header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Menu11Link",
                table: "Header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Menu11Status",
                table: "Header",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Menu12",
                table: "Header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Menu12Link",
                table: "Header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Menu12Status",
                table: "Header",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Menu13",
                table: "Header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Menu13Link",
                table: "Header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Menu13Status",
                table: "Header",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Menu14",
                table: "Header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Menu14Link",
                table: "Header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Menu14Status",
                table: "Header",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Menu15",
                table: "Header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Menu15Link",
                table: "Header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Menu15Status",
                table: "Header",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Menu10",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu10Link",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu10Status",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu11",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu11Link",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu11Status",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu12",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu12Link",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu12Status",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu13",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu13Link",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu13Status",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu14",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu14Link",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu14Status",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu15",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu15Link",
                table: "Header");

            migrationBuilder.DropColumn(
                name: "Menu15Status",
                table: "Header");
        }
    }
}
