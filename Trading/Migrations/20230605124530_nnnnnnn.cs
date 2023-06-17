using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trading.Migrations
{
    /// <inheritdoc />
    public partial class nnnnnnn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SaleObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nomi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HozirgiNarxi = table.Column<int>(type: "int", nullable: false),
                    SotishFarqi = table.Column<int>(type: "int", nullable: false),
                    Amplituda = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traderlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balansi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traderlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Savdolar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleObjectId = table.Column<int>(type: "int", nullable: false),
                    SotibOlindi = table.Column<bool>(type: "bit", nullable: false),
                    XaridNarxi = table.Column<int>(type: "int", nullable: false),
                    XaridSoni = table.Column<int>(type: "int", nullable: false),
                    UmumiyHarajat = table.Column<int>(type: "int", nullable: false),
                    UmumiyTushum = table.Column<int>(type: "int", nullable: false),
                    Foyda = table.Column<int>(type: "int", nullable: false),
                    isOpen = table.Column<bool>(type: "bit", nullable: false),
                    TraderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Savdolar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Savdolar_SaleObjects_SaleObjectId",
                        column: x => x.SaleObjectId,
                        principalTable: "SaleObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Savdolar_Traderlar_TraderId",
                        column: x => x.TraderId,
                        principalTable: "Traderlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Savdolar_SaleObjectId",
                table: "Savdolar",
                column: "SaleObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Savdolar_TraderId",
                table: "Savdolar",
                column: "TraderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Savdolar");

            migrationBuilder.DropTable(
                name: "SaleObjects");

            migrationBuilder.DropTable(
                name: "Traderlar");
        }
    }
}
