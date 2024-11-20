using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinDashboard.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedStockPriceHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holdings_Stock_StockId",
                table: "Holdings");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Portfolios_PortfolioId",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Stock_PortfolioId",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "Stock");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "Holdings",
                newName: "StockID");

            migrationBuilder.RenameColumn(
                name: "HoldingId",
                table: "Holdings",
                newName: "HoldingID");

            migrationBuilder.RenameIndex(
                name: "IX_Holdings_StockId",
                table: "Holdings",
                newName: "IX_Holdings_StockID");

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "Holdings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PortfolioPerformanceHistories",
                columns: table => new
                {
                    PortfolioPerformanceHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PortfolioValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvestedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioPerformanceHistories", x => x.PortfolioPerformanceHistoryID);
                    table.ForeignKey(
                        name: "FK_PortfolioPerformanceHistories_Portfolios_PortfolioID",
                        column: x => x.PortfolioID,
                        principalTable: "Portfolios",
                        principalColumn: "PortfolioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockPriceHistories",
                columns: table => new
                {
                    StockPriceHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPriceHistories", x => x.StockPriceHistoryID);
                    table.ForeignKey(
                        name: "FK_StockPriceHistories_Stock_StockID",
                        column: x => x.StockID,
                        principalTable: "Stock",
                        principalColumn: "StockID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioPerformanceHistories_PortfolioID",
                table: "PortfolioPerformanceHistories",
                column: "PortfolioID");

            migrationBuilder.CreateIndex(
                name: "IX_StockPriceHistories_StockID",
                table: "StockPriceHistories",
                column: "StockID");

            migrationBuilder.AddForeignKey(
                name: "FK_Holdings_Stock_StockID",
                table: "Holdings",
                column: "StockID",
                principalTable: "Stock",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holdings_Stock_StockID",
                table: "Holdings");

            migrationBuilder.DropTable(
                name: "PortfolioPerformanceHistories");

            migrationBuilder.DropTable(
                name: "StockPriceHistories");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Holdings");

            migrationBuilder.RenameColumn(
                name: "StockID",
                table: "Holdings",
                newName: "StockId");

            migrationBuilder.RenameColumn(
                name: "HoldingID",
                table: "Holdings",
                newName: "HoldingId");

            migrationBuilder.RenameIndex(
                name: "IX_Holdings_StockID",
                table: "Holdings",
                newName: "IX_Holdings_StockId");

            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "Stock",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stock_PortfolioId",
                table: "Stock",
                column: "PortfolioId");

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
    }
}
