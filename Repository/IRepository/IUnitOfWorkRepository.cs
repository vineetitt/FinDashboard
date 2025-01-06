namespace FinDashboard.API.Repository.IRepository
{
    public interface IUnitOfWorkRepository: IDisposable
    {
        IUserRepository UserRepository { get; } 
        IPortfolioRepository PortfolioRepository { get; }
        IStockRepository StockRepository { get; }
        IHoldingRepository HoldingRepository { get; }
        IStockPriceHistoryRepository StockPriceHistoryRepository { get; }
        IPortfolioPerformanceHistoryRepository PortfolioPerformanceHistoryRepository { get; }
        Task<int> CompleteAsync();
    }
}
