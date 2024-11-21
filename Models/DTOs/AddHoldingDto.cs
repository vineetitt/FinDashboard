namespace FinDashboard.API.Models.DTOs
{
    public class AddHoldingDto
    {
        public int PortfolioId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public TransactionType TransactionType { get; set; }
    }
    public enum TransactionType
    {
        Buy = 1,
        Sell = 2,
    }
}
