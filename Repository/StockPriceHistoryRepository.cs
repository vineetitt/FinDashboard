using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs.StockPriceHistoryDto;
using FinDashboard.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FinDashboard.API.Repository
{
    public class StockPriceHistoryRepository:IStockPriceHistoryRepository
    {
        private readonly FinDashboardDbContext finDashboardDbContext;

        public StockPriceHistoryRepository(FinDashboardDbContext finDashboardDbContext)
        {
            this.finDashboardDbContext = finDashboardDbContext;
        }

        public IEnumerable<StockPriceHistoryDto> GetStockPriceHistoryByStockID(int stockID, DateTime date)
        {
            DateTime startDate = date.AddDays(-6);
            var stockPriceList = finDashboardDbContext.StockPriceHistories
                .Include(s=>s.Stock)
                .Where(s => s.StockID == stockID && s.Date.Date >= startDate && s.Date<= date.Date)
                .Select(s=>new StockPriceHistoryDto
                {
                    StockID = stockID,
                    StockName = s.Stock.StockName,
                    Date = s.Date,
                    Price = s.Price,
                    Id = s.StockPriceHistoryID
                })
                .ToList();

            if (stockPriceList.Any())
            {
                return stockPriceList;
            }
            else
            {
                return new List<StockPriceHistoryDto>();
            }
        }
    }
}
