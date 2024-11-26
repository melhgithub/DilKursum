using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class namesurnameusername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Isim",
                table: "Kursiyer",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KullaniciAdi",
                table: "Kursiyer",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Soyisim",
                table: "Kursiyer",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Isim",
                table: "Egitmen",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KullaniciAdi",
                table: "Egitmen",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Soyisim",
                table: "Egitmen",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isim",
                table: "Kursiyer");

            migrationBuilder.DropColumn(
                name: "KullaniciAdi",
                table: "Kursiyer");

            migrationBuilder.DropColumn(
                name: "Soyisim",
                table: "Kursiyer");

            migrationBuilder.DropColumn(
                name: "Isim",
                table: "Egitmen");

            migrationBuilder.DropColumn(
                name: "KullaniciAdi",
                table: "Egitmen");

            migrationBuilder.DropColumn(
                name: "Soyisim",
                table: "Egitmen");
        }
    }
}
