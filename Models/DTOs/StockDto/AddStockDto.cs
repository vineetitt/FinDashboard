namespace FinDashboard.API.Models.DTOs.AssetDto
{
    public class AddStockDto
    {
        public string StockName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        //public decimal CurrentPrice { get; set; }
        //public decimal HighPrice { get; set; }
        //public decimal LowPrice { get; set; }
        //public decimal OpenPrice { get; set; }
        //public decimal ClosePrice { get; set; }
    }
}
