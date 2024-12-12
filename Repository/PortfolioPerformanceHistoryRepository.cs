using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs.PortfolioPerformanceHistoryDto;
using FinDashboard.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FinDashboard.API.Repository
{
    public class PortfolioPerformanceHistoryRepository : IPortfolioPerformanceHistoryRepository
    {
        private readonly FinDashboardDbContext finDashboardDbContext;

        public PortfolioPerformanceHistoryRepository(FinDashboardDbContext finDashboardDbContext)
        {
            this.finDashboardDbContext = finDashboardDbContext;
        }
        public bool AddPortfolioPerformancePrice(int portfolioId, int portfolioValue, int investedValue)
        {
            if (portfolioId == 0 || portfolioValue == 0 || investedValue == 0)
            {
                throw new Exception("Cannot add because values are required, and these fields are empty or invalid.");
            }

            var today = DateTime.UtcNow.Date;

            var portfolioPerformance = finDashboardDbContext.PortfolioPerformanceHistories
                .FirstOrDefault(p => p.PortfolioID == portfolioId && p.Date == today);

            if (portfolioPerformance == null)
            {
                var newPerformanceEntry = new PortfolioPerformanceHistory
                {
                    PortfolioID = portfolioId,
                    PortfolioValue = portfolioValue,
                    InvestedValue = investedValue,
                    Date = today 
                };

                finDashboardDbContext.PortfolioPerformanceHistories.Add(newPerformanceEntry);
            }
            else
            {
                portfolioPerformance.PortfolioValue = portfolioValue;
                portfolioPerformance.InvestedValue = investedValue;
            }

            finDashboardDbContext.SaveChanges();

            return true;
        }

        public IEnumerable<GetPortfolioPerformanceDto> GetPortfolioPerformancePriceHistory(int portfolioId, DateTime date)
        {
            DateTime startDate = date.AddDays(-1);
            var portfolioPerformanceHistory = finDashboardDbContext.PortfolioPerformanceHistories
                .Where(p => p.PortfolioID == portfolioId && p.Date >= startDate && p.Date <= date)
                .Select(p => new GetPortfolioPerformanceDto
                {
                    PortfolioID = p.PortfolioID,
                    InvestedValue = p.InvestedValue,
                    PortfolioValue = p.PortfolioValue,
                    Date = p.Date
                }).ToList();
            if (portfolioPerformanceHistory.Any())
            {
                return portfolioPerformanceHistory;
            }
            else
            {
                throw new CustomException("No data found ", 200);
            }

        }
    }
}
