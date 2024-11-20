using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinDashboard.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewModelsAndChangedOlderOnesAndAddedDbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Portfolios_PortfolioId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Holding_Assets_StockId",
                table: "Holding");

            migrationBuilder.DropForeignKey(
                name: "FK_Holding_Portfolios_PortfolioID",
                table: "Holding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Holding",
                table: "Holding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.RenameTable(
                name: "Holding",
                newName: "Holdings");

            migrationBuilder.RenameTable(
                name: "Assets",
                newName: "Stock");

            migrationBuilder.RenameIndex(
                name: "IX_Holding_StockId",
                table: "Holdings",
                newName: "IX_Holdings_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_Holding_PortfolioID",
                table: "Holdings",
                newName: "IX_Holdings_PortfolioID");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_PortfolioId",
                table: "Stock",
                newName: "IX_Stock_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Holdings",
                table: "Holdings",
                column: "HoldingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "StockID");

            migrationBuilder.AddForeignKey(
                name: "FK_Holdings_Portfolios_PortfolioID",
                table: "Holdings",
                column: "PortfolioID",
                principalTable: "Portfolios",
                principalColumn: "PortfolioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holdings_Stock_StockId",
                table: "Holdings",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Portfolios_PortfolioId",
                table: "Stock",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "PortfolioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holdings_Portfolios_PortfolioID",
                table: "Holdings");

            migrationBuilder.DropForeignKey(
                name: "FK_Holdings_Stock_StockId",
                table: "Holdings");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Portfolios_PortfolioId",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Holdings",
                table: "Holdings");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "Assets");

            migrationBuilder.RenameTable(
                name: "Holdings",
                newName: "Holding");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_PortfolioId",
                table: "Assets",
                newName: "IX_Assets_PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_Holdings_StockId",
                table: "Holding",
                newName: "IX_Holding_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_Holdings_PortfolioID",
                table: "Holding",
                newName: "IX_Holding_PortfolioID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "StockID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Holding",
                table: "Holding",
                column: "HoldingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Portfolios_PortfolioId",
                table: "Assets",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holding_Assets_StockId",
                table: "Holding",
                column: "StockId",
                principalTable: "Assets",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holding_Portfolios_PortfolioID",
                table: "Holding",
                column: "PortfolioID",
                principalTable: "Portfolios",
                principalColumn: "PortfolioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
