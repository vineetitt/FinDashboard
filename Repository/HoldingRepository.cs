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
            var user = finDashboardDbContext.Users.FirstOrDefault(u => u.UserID == addHoldingDto.UserId);
            finDashboardDbContext.Users
                .Include(u => u.Portfolio)
                .FirstOrDefault(u => u.UserID == addHoldingDto.UserId);
            var portfolio = user.Portfolio;
            var stock = finDashboardDbContext.Stock.FirstOrDefault(s => s.StockID == addHoldingDto.StockId);

            if(stock == null)
            {
                throw new CustomException($"Stock is not in the list ", 404);
            }
            var existingHolding = finDashboardDbContext.Holdings.FirstOrDefault(h => h.PortfolioID == portfolio.PortfolioId && h.StockID == addHoldingDto.StockId);
            if (existingHolding != null)
            {
                existingHolding.Quantity += addHoldingDto.Quantity;
                existingHolding.TotalInvested += addHoldingDto.Quantity * stock.CurrentPrice;
                existingHolding.CurrentPrice = stock.CurrentPrice;
            }
            else
            {
                var newHolding = new Holding
                {
                    StockID = stock.StockID,
                    Quantity = addHoldingDto.Quantity,
                    PurchasePrice = stock.CurrentPrice,
                    TotalInvested = addHoldingDto.Quantity * stock.CurrentPrice,
                    PortfolioID = portfolio.PortfolioId,
                    CurrentPrice = stock.CurrentPrice
                };
                finDashboardDbContext.Holdings.Add(newHolding);
                stock.Quantity-=addHoldingDto.Quantity;
            }
            portfolio.InvestedValue += addHoldingDto.Quantity * stock.CurrentPrice;
            portfolio.CurrentValue += addHoldingDto.Quantity * stock.CurrentPrice;
            finDashboardDbContext.SaveChanges();
            var transaction = new Transaction()
            {
                Quantity = addHoldingDto.Quantity,
                PricePerUnit = stock.CurrentPrice,
                TransactioDate = DateTime.Now,
                TransactionType = "Buy",
                PortfolioID = portfolio.PortfolioId,
                StockID = stock.StockID,
            };
            finDashboardDbContext.Transactions.Add(transaction);
            finDashboardDbContext.SaveChanges();
        }

        public bool SellUserStock(AddHoldingDto addHoldingDto)
        {
            var user = finDashboardDbContext.Users
                        .Include(p => p.Portfolio)
                            .ThenInclude(h => h.Holdings)
                        .FirstOrDefault(u => u.UserID == addHoldingDto.UserId);
            
            if (user == null)
            {
                throw new CustomException("User not found", 404);
            }

            
            var portfolio = user.Portfolio;

            if (portfolio == null)
            {
                throw new CustomException("Portfolio not found for the user", 404);
            }
            var holding = portfolio.Holdings.FirstOrDefault(h => h.StockID == addHoldingDto.StockId);
            if (holding == null)
            {
                throw new CustomException("Holding for the specified stock not found", 404);
            }

            if (holding.Quantity < addHoldingDto.Quantity)
            {
                throw new CustomException("Cannot sell more quantity than you hold", 400);
            }
            holding.Quantity -= addHoldingDto.Quantity;
            var returnReceived = addHoldingDto.Quantity * holding.CurrentPrice;
            var priceToDeduct = addHoldingDto.Quantity * holding.PurchasePrice;
            holding.TotalInvested -= priceToDeduct;
            portfolio.InvestedValue -= priceToDeduct;
            portfolio.CurrentValue -= returnReceived;

            if (holding.Quantity == 0)
            {
                portfolio.Holdings.Remove(holding);
            }

            var transaction = new Transaction()
            {
                Quantity = addHoldingDto.Quantity,
                PricePerUnit = holding.CurrentPrice,
                TransactioDate = DateTime.Now,
                TransactionType = "Sell",
                PortfolioID = portfolio.PortfolioId,
                StockID = holding.StockID,
            };
            finDashboardDbContext.Transactions.Add(transaction);
            finDashboardDbContext.SaveChanges();
            return true;
        }

    }
}
