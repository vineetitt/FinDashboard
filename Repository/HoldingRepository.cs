using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs;
using FinDashboard.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FinDashboard.API.Repository
{
    public class HoldingRepository: IHoldingRepository
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

            if (existingHolding != null)
            {

                var newTotalInvested = existingHolding.TotalInvested + (addHoldingDto.Quantity * addHoldingDto.PurchasePrice);
                var newQuantity = existingHolding.Quantity + addHoldingDto.Quantity;

                existingHolding.TotalInvested = newTotalInvested;
                existingHolding.Quantity = newQuantity;
                existingHolding.PurchasePrice = newTotalInvested / newQuantity;

                portfolio.InvestedValue += addHoldingDto.Quantity * addHoldingDto.PurchasePrice;
                finDashboardDbContext.Holdings.Update(existingHolding);
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
    }
}
