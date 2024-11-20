namespace FinDashboard.API.Models.Domain
{
    public class Holding
    {
        public int HoldingID { get; set; }
        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal TotalInvested { get; set; }

        public int PortfolioID { get; set; }
        public virtual Portfolio Portfolio { get; set; }

        public int StockID { get; set; }
        public virtual Stock Stock { get; set; }

    }
}
