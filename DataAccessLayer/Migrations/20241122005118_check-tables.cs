using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class checktables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_KursFile_EgitmenID",
                table: "KursFile",
                column: "EgitmenID");

            migrationBuilder.AddForeignKey(
                name: "FK_KursFile_Egitmen_EgitmenID",
                table: "KursFile",
                column: "EgitmenID",
                principalTable: "Egitmen",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KursFile_Egitmen_EgitmenID",
                table: "KursFile");

            migrationBuilder.DropIndex(
                name: "IX_KursFile_EgitmenID",
                table: "KursFile");
        }
    }
}
