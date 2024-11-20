using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs.AssetDto;
using FinDashboard.API.Models.DTOs.StockDto;
using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace FinDashboard.API.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly FinDashboardDbContext finDashboardDbContext;

        public StockRepository(FinDashboardDbContext finDashboardDbContext)
        {
            this.finDashboardDbContext = finDashboardDbContext;
        }

        public bool AddStock(AddStockDto addStockDto)
        {
            if (addStockDto.StockName == "")
            {
                throw new CustomException("Fields are empty", 400);
            }
            if (addStockDto.Quantity < 0)
            {
                throw new CustomException("Quantity must be either 0 or greater than 0", 400);
            }


            var existingStock = finDashboardDbContext.Stock.FirstOrDefault(stock => stock.StockName == addStockDto.StockName);

            if (existingStock != null)
            {
                existingStock.Quantity = existingStock.Quantity + addStockDto.Quantity;
            }
            else
            {
                var stock = new Stock()
                {
                    StockName = addStockDto.StockName,
                    Quantity = addStockDto.Quantity,
                };

                finDashboardDbContext.Stock.Add(stock);
            }
            finDashboardDbContext.SaveChanges();
            return true;

        }

        public bool UpdateStock(int stockId, UpdateStockDto updateStockDto)
        {
            if (updateStockDto == null)
            {
                throw new CustomException("Invalid update data.", 400);
            }

            var getStock = finDashboardDbContext.Stock.FirstOrDefault(stock => stock.StockID == stockId);
            if (getStock != null)
            {
                if (updateStockDto.Quantity.HasValue && updateStockDto.Quantity.Value > 0)
                {
                    getStock.Quantity = updateStockDto.Quantity.Value;
                }
                else if (updateStockDto.Quantity.HasValue)
                {
                    throw new ArgumentException("Quantity must be greater than 0.");
                }

                if (!string.IsNullOrEmpty(updateStockDto.StockName))
                {
                    getStock.StockName = updateStockDto.StockName;
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
