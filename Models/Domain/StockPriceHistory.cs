namespace FinDashboard.API.Models.Domain
{
    public class StockPriceHistory
    {
            public int StockPriceHistoryID { get; set; }
            public int StockID { get; set; }
            public DateTime Date { get; set; }
            public decimal Price { get; set; }
            public virtual Stock Stock { get; set; }

    }
}
