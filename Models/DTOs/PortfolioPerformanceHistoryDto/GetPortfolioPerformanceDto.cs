namespace FinDashboard.API.Models.DTOs.PortfolioPerformanceHistoryDto
{
    public class GetPortfolioPerformanceDto
    {
        public int PortfolioID { get; set; }
        public decimal PortfolioValue { get; set; }
        public decimal InvestedValue { get; set; }
        public DateTime Date { get; set; }
    }
}
