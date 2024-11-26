using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class includes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_KursiyerKursDetail_KursID",
                table: "KursiyerKursDetail",
                column: "KursID");

            migrationBuilder.CreateIndex(
                name: "IX_KursiyerKursDetail_KursiyerID",
                table: "KursiyerKursDetail",
                column: "KursiyerID");

            migrationBuilder.CreateIndex(
                name: "IX_KursDetail_KursID",
                table: "KursDetail",
                column: "KursID");

            migrationBuilder.CreateIndex(
                name: "IX_Kurs_DilID",
                table: "Kurs",
                column: "DilID");

            migrationBuilder.CreateIndex(
                name: "IX_Kurs_EgitmenID",
                table: "Kurs",
                column: "EgitmenID");

            migrationBuilder.AddForeignKey(
                name: "FK_Kurs_Dil_DilID",
                table: "Kurs",
                column: "DilID",
                principalTable: "Dil",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kurs_Egitmen_EgitmenID",
                table: "Kurs",
                column: "EgitmenID",
                principalTable: "Egitmen",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KursDetail_Kurs_KursID",
                table: "KursDetail",
                column: "KursID",
                principalTable: "Kurs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KursiyerKursDetail_Kurs_KursID",
                table: "KursiyerKursDetail",
                column: "KursID",
                principalTable: "Kurs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KursiyerKursDetail_Kursiyer_KursiyerID",
                table: "KursiyerKursDetail",
                column: "KursiyerID",
                principalTable: "Kursiyer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kurs_Dil_DilID",
                table: "Kurs");

            migrationBuilder.DropForeignKey(
                name: "FK_Kurs_Egitmen_EgitmenID",
                table: "Kurs");

            migrationBuilder.DropForeignKey(
                name: "FK_KursDetail_Kurs_KursID",
                table: "KursDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_KursiyerKursDetail_Kurs_KursID",
                table: "KursiyerKursDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_KursiyerKursDetail_Kursiyer_KursiyerID",
                table: "KursiyerKursDetail");

            migrationBuilder.DropIndex(
                name: "IX_KursiyerKursDetail_KursID",
                table: "KursiyerKursDetail");

            migrationBuilder.DropIndex(
                name: "IX_KursiyerKursDetail_KursiyerID",
                table: "KursiyerKursDetail");

            migrationBuilder.DropIndex(
                name: "IX_KursDetail_KursID",
                table: "KursDetail");

            migrationBuilder.DropIndex(
                name: "IX_Kurs_DilID",
                table: "Kurs");

            migrationBuilder.DropIndex(
                name: "IX_Kurs_EgitmenID",
                table: "Kurs");
        }
    }
}
