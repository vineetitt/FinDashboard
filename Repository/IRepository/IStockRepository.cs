using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs.AssetDto;
using FinDashboard.API.Models.DTOs.StockDto;
using Microsoft.AspNetCore.Mvc;

namespace FinDashboard.API.Repository.IRepository
{
    public interface IStockRepository
    {
        bool AddStock(AddStockDto addAssetDto);
        bool UpdateStock(int stockId, UpdateStockDto updateStockDto);
        bool DeleteStock(int stockId);
        List<Stock> GetAllStock();
    }
}
