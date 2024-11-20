using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinDashboard.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewModelsAndChangedOlderOnes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Portfolios_PortfolioID",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "PortfolioID",
                table: "Assets",
                newName: "PortfolioId");

            migrationBuilder.RenameColumn(
                name: "AssetName",
                table: "Assets",
                newName: "StockName");

            migrationBuilder.RenameColumn(
                name: "AssetID",
                table: "Assets",
                newName: "StockID");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_PortfolioID",
                table: "Assets",
                newName: "IX_Assets_PortfolioId");

            migrationBuilder.AlterColumn<int>(
                name: "PortfolioId",
                table: "Assets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Holding",
                columns: table => new
                {
                    HoldingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalInvested = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PortfolioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holding", x => x.HoldingId);
                    table.ForeignKey(
                        name: "FK_Holding_Assets_StockId",
                        column: x => x.StockId,
                        principalTable: "Assets",
                        principalColumn: "StockID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Holding_Portfolios_PortfolioID",
                        column: x => x.PortfolioID,
                        principalTable: "Portfolios",
                        principalColumn: "PortfolioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Holding_PortfolioID",
                table: "Holding",
                column: "PortfolioID");

            migrationBuilder.CreateIndex(
                name: "IX_Holding_StockId",
                table: "Holding",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Portfolios_PortfolioId",
                table: "Assets",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "PortfolioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Portfolios_PortfolioId",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "Holding");

            migrationBuilder.RenameColumn(
                name: "PortfolioId",
                table: "Assets",
                newName: "PortfolioID");

            migrationBuilder.RenameColumn(
                name: "StockName",
                table: "Assets",
                newName: "AssetName");

            migrationBuilder.RenameColumn(
                name: "StockID",
                table: "Assets",
                newName: "AssetID");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_PortfolioId",
                table: "Assets",
                newName: "IX_Assets_PortfolioID");

            migrationBuilder.AlterColumn<int>(
                name: "PortfolioID",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Portfolios_PortfolioID",
                table: "Assets",
                column: "PortfolioID",
                principalTable: "Portfolios",
                principalColumn: "PortfolioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
