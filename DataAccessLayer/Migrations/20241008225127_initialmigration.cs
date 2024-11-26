using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dil",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DilAdi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dil", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Egitmen",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    Bakiye = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Egitmen", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kurs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KursAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KursAciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DersSayisi = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<float>(type: "real", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    EgitmenID = table.Column<int>(type: "int", nullable: false),
                    DilID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KursDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoLink = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    KursID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursDetail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kursiyer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bakiye = table.Column<float>(type: "real", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kursiyer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KursiyerKursDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    KursID = table.Column<int>(type: "int", nullable: false),
                    KursiyerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursiyerKursDetail", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Dil");

            migrationBuilder.DropTable(
                name: "Egitmen");

            migrationBuilder.DropTable(
                name: "Kurs");

            migrationBuilder.DropTable(
                name: "KursDetail");

            migrationBuilder.DropTable(
                name: "Kursiyer");

            migrationBuilder.DropTable(
                name: "KursiyerKursDetail");
        }
    }
}
