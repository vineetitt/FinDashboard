namespace FinDashboard.API.Models.Domain
{
    public class PortfolioPerformanceHistory
    {
        public int PortfolioPerformanceHistoryID { get; set; }
        public int PortfolioID { get; set; }
        public DateTime Date { get; set; }
        public decimal PortfolioValue { get; set; }
        public decimal InvestedValue { get; set; }

        public virtual Portfolio Portfolio { get; set; }
    }
}
