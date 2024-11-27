using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs.StockPriceHistoryDto;

namespace FinDashboard.API.Repository.IRepository
{
    public interface IStockPriceHistoryRepository
    {
        IEnumerable<StockPriceHistoryDto> GetStockPriceHistoryByStockID(int stockID, DateTime date);
    }
}
