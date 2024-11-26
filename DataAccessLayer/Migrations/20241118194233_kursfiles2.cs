using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class kursfiles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "KursFile");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "KursFile",
                newName: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "KursFile",
                newName: "FilePath");

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "KursFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
