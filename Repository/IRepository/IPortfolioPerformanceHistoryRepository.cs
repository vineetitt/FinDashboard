using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs.PortfolioPerformanceHistoryDto;

namespace FinDashboard.API.Repository.IRepository
{
    public interface IPortfolioPerformanceHistoryRepository
    {
         bool AddPortfolioPerformancePrice(int portfolioId, int portfolioValue, int investedValue);

        IEnumerable<GetPortfolioPerformanceDto> GetPortfolioPerformancePriceHistory(int portfolioId, DateTime date);
    }
}
