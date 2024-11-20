namespace FinDashboard.API.Models.Domain
{
    public class Stock
    {
        public int StockID { get; set; }  
        public string StockName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal CurrentPrice { get; set; } = 0;
        public decimal HighPrice { get; set; } = 0;
        public decimal LowPrice { get; set; } = 0;
        public decimal OpenPrice { get; set; } = 0;
        public decimal ClosePrice { get; set; }

    }
}
