namespace FinDashboard.API.Models.DTOs.AssetDto
{
    public class AddStockDto
    {
        public string StockName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
    }
}
