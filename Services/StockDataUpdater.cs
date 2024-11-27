using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FinDashboard.API.Services;
using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;

namespace FinDashboard.API.Services
{
    public class StockDataUpdater : BackgroundService
    {
        private readonly ILogger<StockDataUpdater> _logger;
        private readonly FinHubService _finHubService;
        private readonly IServiceProvider _serviceProvider;

        public StockDataUpdater(ILogger<StockDataUpdater> logger, FinHubService finHubService, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _finHubService = finHubService;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("StockDataUpdater background service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<FinDashboardDbContext>();

                        var stocks = dbContext.Stock.ToList();
                        var portfolioes = dbContext.Portfolios.ToList();
                        var today = DateTime.UtcNow.Date;

                        foreach (var stock in stocks)
                        {
                            var stockData = await _finHubService.GetCurrentStockPriceAsync(stock.StockName);

                            stock.CurrentPrice = stockData.c;
                            stock.OpenPrice = stockData.o;
                            stock.HighPrice = stockData.h;
                            stock.LowPrice = stockData.l;
                            stock.ClosePrice = stockData.pc;

                            var existingHistory = dbContext.StockPriceHistories.FirstOrDefault(sph => sph.StockID == stock.StockID && sph.Date == today);

                            if (existingHistory != null)
                            {
                                existingHistory.Price = stock.CurrentPrice;
                            }
                            else
                            {

                                var newHistory = new StockPriceHistory
                                {
                                    StockID = stock.StockID,
                                    Date = today,
                                    Price = stock.CurrentPrice
                                };
                                dbContext.StockPriceHistories.Add(newHistory);

                            }

                            var holdings = dbContext.Holdings.Where(h => h.StockID == stock.StockID).ToList();

                            foreach (var holding in holdings)
                            {
                                holding.CurrentPrice = stock.CurrentPrice;
                            }
                        }

                        await dbContext.SaveChangesAsync(stoppingToken);

                        foreach (var portfolio in portfolioes)
                        {
                            var portfolioHoldings = dbContext.Holdings.Where(ph => ph.PortfolioID == portfolio.PortfolioId).ToList();
                            decimal currentValue = portfolioHoldings.Sum(ph => ph.CurrentPrice * ph.Quantity);
                            portfolio.CurrentValue = currentValue;
                        }
                        await dbContext.SaveChangesAsync(stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating stock data.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
