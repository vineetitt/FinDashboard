using FinDashboard.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FinDashboard.API.Data
{
    public class FinDashboardDbContext : DbContext
    {
        public FinDashboardDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Holding> Holdings { get; set; }
        public DbSet<PortfolioPerformanceHistory> PortfolioPerformanceHistories { get; set; }
        public DbSet<StockPriceHistory> StockPriceHistories { get; set; }

    }
}
