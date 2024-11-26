using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class header : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Header",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Menu1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu1Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu1Status = table.Column<bool>(type: "bit", nullable: false),
                    Menu2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu2Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu2Status = table.Column<bool>(type: "bit", nullable: false),
                    Menu3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu3Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu3Status = table.Column<bool>(type: "bit", nullable: false),
                    Menu4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu4Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu4Status = table.Column<bool>(type: "bit", nullable: false),
                    Menu5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu5Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu5Status = table.Column<bool>(type: "bit", nullable: false),
                    Menu6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu6Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu6Status = table.Column<bool>(type: "bit", nullable: false),
                    Menu7 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu7Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu7Status = table.Column<bool>(type: "bit", nullable: false),
                    Menu8 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu8Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu8Status = table.Column<bool>(type: "bit", nullable: false),
                    Menu9 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu9Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu9Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Header", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Header");
        }
    }
}
