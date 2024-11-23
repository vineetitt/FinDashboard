using FinDashboard.API.Models.Domain;

namespace FinDashboard.API.Models.DTOs.HoldingDto
{
    public class SellStockDto
    {
        public int PortfolioID { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }

    }
}
