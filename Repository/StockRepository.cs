using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs.AssetDto;
using FinDashboard.API.Models.DTOs.StockDto;
using FinDashboard.API.Repository.IRepository;
using FinDashboard.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinDashboard.API.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly FinDashboardDbContext finDashboardDbContext;
        private readonly FinHubService finHubService;

        public StockRepository(FinDashboardDbContext finDashboardDbContext, FinHubService finHubService)
        {
            this.finDashboardDbContext = finDashboardDbContext;
            this.finHubService = finHubService;
        }

        public async Task<bool> AddStock(AddStockDto addStockDto)
        {
            if (addStockDto.StockName == "")
            {
                throw new CustomException("Stock name cannot be empty", 400);
            }
            if (addStockDto.Quantity < 0)
            {
                throw new CustomException("Quantity must be either 0 or greater than 0", 400);
            }

            var stockData = await finHubService.GetCurrentStockPriceAsync(addStockDto.StockName);
            var existingStock = finDashboardDbContext.Stock.FirstOrDefault(stock => stock.StockName == addStockDto.StockName);

            if (existingStock != null)
            {
                existingStock.Quantity = existingStock.Quantity + addStockDto.Quantity;
                existingStock.OpenPrice = stockData.o;
                existingStock.HighPrice = stockData.h;
                existingStock.LowPrice = stockData.l;
                existingStock.CurrentPrice = stockData.c;
                existingStock.ClosePrice = stockData.pc;
            }
            else
            {
                var stock = new Stock()
                {
                    StockName = addStockDto.StockName,
                    Quantity = addStockDto.Quantity,
                    CurrentPrice = stockData.c,
                    OpenPrice = stockData.o,
                    ClosePrice = stockData.pc,
                    HighPrice = stockData.h,
                    LowPrice = stockData.l,
                };

                finDashboardDbContext.Stock.Add(stock);
            }
            finDashboardDbContext.SaveChanges();
            return true;

        }

        public async Task<bool> UpdateStock(int stockId, UpdateStockDto updateStockDto)
        {
            if (updateStockDto == null)
            {
                throw new CustomException("Invalid update data.", 400);
            }

            var getStock = finDashboardDbContext.Stock.FirstOrDefault(stock => stock.StockID == stockId);
            
            var stockData = await finHubService.GetCurrentStockPriceAsync(getStock.StockName);

            if (getStock != null)
            {
                if (updateStockDto.Quantity.HasValue && updateStockDto.Quantity.Value > 0)
                {
                    getStock.Quantity = updateStockDto.Quantity.Value;
                    getStock.CurrentPrice = stockData.c;
                    getStock.OpenPrice = stockData.o;
                    getStock.ClosePrice = stockData.pc;
                    getStock.HighPrice = stockData.h;
                    getStock.LowPrice = stockData.l;

                }
                else if (updateStockDto.Quantity.HasValue)
                {
                    throw new ArgumentException("Quantity must be greater than 0.");
                }

                if (!string.IsNullOrEmpty(updateStockDto.StockName))
                {
                    getStock.StockName = updateStockDto.StockName;
                    getStock.CurrentPrice = stockData.c;
                    getStock.OpenPrice = stockData.o;
                    getStock.ClosePrice = stockData.pc;
                    getStock.HighPrice = stockData.h;
                    getStock.LowPrice = stockData.l;

                }
                finDashboardDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteStock(int stockId)
        {
            if (stockId < 0)
            {
                throw new CustomException("Please enter valid stock Id", 400);
            }
            var getStock = finDashboardDbContext.Stock.FirstOrDefault(stock => stock.StockID == stockId);
            if (getStock != null)
            {
                finDashboardDbContext.Stock.Remove(getStock);
                finDashboardDbContext.SaveChanges();
                return true;
            }
            else
            {
                throw new CustomException($"User with is StockId: {stockId} not found", 200);
            }
        }
        public List<Stock> GetAllStock()
        {
            var allStocks = finDashboardDbContext.Stock.ToList();
            return allStocks;
        }

       
    }
}
