using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinDashboard.API.Migrations
{
    /// <inheritdoc />
    public partial class addedCurrentPriceinHoldingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPrice",
                table: "Holdings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "Holdings");
        }
    }
}
