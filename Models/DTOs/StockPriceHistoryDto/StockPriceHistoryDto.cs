namespace FinDashboard.API.Models.DTOs.StockPriceHistoryDto
{
    public class StockPriceHistoryDto
    {
        public int Id { get; set; }
        public int StockID { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public string StockName { get; set; }
    }
}
