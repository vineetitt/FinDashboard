using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs;
using FinDashboard.API.Models.DTOs.HoldingDto;
using FinDashboard.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FinDashboard.API.Repository
{
    public class HoldingRepository : IHoldingRepository
    {
        private readonly FinDashboardDbContext finDashboardDbContext;

        public HoldingRepository(FinDashboardDbContext finDashboardDbContext)
        {
            this.finDashboardDbContext = finDashboardDbContext;
        }

        public void BuyStock(AddHoldingDto addHoldingDto)
        {
            var portfolio = finDashboardDbContext.Portfolios.FirstOrDefault(p => p.PortfolioId == addHoldingDto.PortfolioId);
            var existingHolding = finDashboardDbContext.Holdings.FirstOrDefault(h => h.PortfolioID == addHoldingDto.PortfolioId && h.StockID == addHoldingDto.StockId);
            var stock = finDashboardDbContext.Stock.FirstOrDefault(s => s.StockID == addHoldingDto.StockId);
            var allStock = finDashboardDbContext.Stock.ToList();
            if (existingHolding != null)
            {

                var newTotalInvested = existingHolding.TotalInvested + (addHoldingDto.Quantity * addHoldingDto.PurchasePrice);
                
                var newQuantity = existingHolding.Quantity + addHoldingDto.Quantity;

                existingHolding.TotalInvested = newTotalInvested;
                existingHolding.Quantity = newQuantity;
                existingHolding.PurchasePrice = newTotalInvested / newQuantity;

                portfolio.InvestedValue += addHoldingDto.Quantity * addHoldingDto.PurchasePrice;
                finDashboardDbContext.Holdings.Update(existingHolding);
                
                stock.Quantity = stock.Quantity - addHoldingDto.Quantity;
            }
            else
            {
                var newHolding = new Holding
                {
                    PortfolioID = addHoldingDto.PortfolioId,
                    StockID = addHoldingDto.StockId,
                    Quantity = addHoldingDto.Quantity,
                    PurchasePrice = addHoldingDto.PurchasePrice,
                    TotalInvested = addHoldingDto.Quantity * addHoldingDto.PurchasePrice
                };

                finDashboardDbContext.Holdings.Add(newHolding);
                portfolio.InvestedValue += newHolding.TotalInvested;
                stock.Quantity -= addHoldingDto.Quantity;
            }
            finDashboardDbContext.Portfolios.Update(portfolio);
            var transaction = new Transaction
            {
                PortfolioID = addHoldingDto.PortfolioId,
                StockID = addHoldingDto.StockId,
                Quantity = addHoldingDto.Quantity,
                PricePerUnit = addHoldingDto.PurchasePrice,
                TransactionType = "Buy",
                TransactioDate = DateTime.UtcNow
            };
            finDashboardDbContext.Transactions.Add(transaction);
            finDashboardDbContext.SaveChanges();
        }

        //public void SellStock(SellStockDto sellStockDto)
        //{
        //    var portfolio = finDashboardDbContext.Portfolios
        //        .Include(p => p.Holdings)
        //        .FirstOrDefault(p => p.PortfolioId == sellStockDto.PortfolioID);
        //    if (portfolio == null)
        //        throw new CustomException("Portfolio not found", 400);

        //    var holding = portfolio.Holdings.FirstOrDefault(h => h.StockID == sellStockDto.StockId);
        //    if (holding == null)
        //        throw new CustomException("Holding not found.", 400);

        //    if (sellStockDto.Quantity <= 0)
        //        throw new CustomException("Holding not found.", 400);

        //    if (sellStockDto.Quantity > holding.Quantity)
        //        throw new CustomException("Holding not found.", 400);

        //    var transaction = new Transaction
        //    {
        //        StockID = sellStockDto.StockId,
        //        PortfolioID = sellStockDto.PortfolioID,
        //        TransactionType = "Sell",
        //        Quantity = sellStockDto.Quantity,
        //        PricePerUnit = sellStockDto.PurchasePrice,
        //        TransactioDate = DateTime.UtcNow
        //    };
        //    finDashboardDbContext.Transactions.Add(transaction);

        //    decimal totalSaleAmount = sellStockDto.Quantity * sellStockDto.PurchasePrice;//assuming purchase price and selling current price
        //    decimal proportion = sellStockDto.Quantity / holding.Quantity;
        //    decimal totalInvestedForSoldShares = holding.TotalInvested * proportion;
        //    decimal profitOrLoss = totalSaleAmount - totalInvestedForSoldShares;

        //    holding.Quantity -= sellStockDto.Quantity;
        //    holding.TotalInvested -= totalInvestedForSoldShares;

        //    if (holding.Quantity == 0)
        //    {
        //        finDashboardDbContext.Holdings.Remove(holding);
        //    }
        //    portfolio.InvestedValue -= totalInvestedForSoldShares;
        //    portfolio.CurrentValue -= totalInvestedForSoldShares;
        //    finDashboardDbContext.SaveChanges();
        //}
    }
}
