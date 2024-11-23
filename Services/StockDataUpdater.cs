using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FinDashboard.API.Services;
using FinDashboard.API.Data;

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

                        // Fetch all stocks from the database
                        var stocks = dbContext.Stock.ToList();

                        foreach (var stock in stocks)
                        {
                            var stockData = await _finHubService.GetCurrentStockPriceAsync(stock.StockName);

                            // Update stock details
                            stock.CurrentPrice = stockData.c;
                            stock.OpenPrice = stockData.o;
                            stock.HighPrice = stockData.h;
                            stock.LowPrice = stockData.l;
                            stock.ClosePrice = stockData.pc;

                            _logger.LogInformation($"Updated {stock.StockName}: Current={stock.CurrentPrice}, High={stock.HighPrice}, Low={stock.LowPrice}");
                        }

                        // Save changes to the database
                        await dbContext.SaveChangesAsync(stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating stock data.");
                }

                // Wait for 1 minute
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
