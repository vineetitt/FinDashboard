using FinDashboard.API.Models.Domain;

namespace FinDashboard.API.Models.DTOs
{
    public class AddHoldingDto
    {
        public int PortfolioId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal TotalInvested => PurchasePrice * Quantity;
        //public decimal ProfitLoss => (Quantity * Stock.CurrentPrice) - TotalInvested;
        //public virtual Stock Stock { get; set; }

    }
}
