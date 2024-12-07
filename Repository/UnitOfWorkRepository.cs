using FinDashboard.API.Data;
using FinDashboard.API.Repository.IRepository;

namespace FinDashboard.API.Repository
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly FinDashboardDbContext _context;

        public IUserRepository UserRepository { get; }
        public IPortfolioRepository PortfolioRepository { get; }
        public IStockRepository StockRepository { get; }
        public IHoldingRepository HoldingRepository { get; }
        public IStockPriceHistoryRepository StockPriceHistoryRepository { get; }

        public UnitOfWorkRepository(
        FinDashboardDbContext context,
        IUserRepository userRepository,
        IPortfolioRepository portfolioRepository,
        IStockRepository stockRepository,
        IHoldingRepository holdingRepository,
        IStockPriceHistoryRepository stockPriceHistoryRepository)
        {
            _context = context;
            UserRepository = userRepository;
            PortfolioRepository = portfolioRepository;
            StockRepository = stockRepository;
            HoldingRepository = holdingRepository;
            StockPriceHistoryRepository = stockPriceHistoryRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
