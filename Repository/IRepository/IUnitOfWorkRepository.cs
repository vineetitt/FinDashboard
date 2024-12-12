namespace FinDashboard.API.Repository.IRepository
{
    public interface IUnitOfWorkRepository: IDisposable
    {
        IUserRepository UserRepository { get; } //HERE I NEED TO UNDERSTAND THAT IT WILL RETURN INSTANCE OF A CLASS IMPLEMENTING THIS INTERFACE
        IPortfolioRepository PortfolioRepository { get; }
        IStockRepository StockRepository { get; }
        IHoldingRepository HoldingRepository { get; }
        IStockPriceHistoryRepository StockPriceHistoryRepository { get; }
        IPortfolioPerformanceHistoryRepository PortfolioPerformanceHistoryRepository { get; }
        Task<int> CompleteAsync();
    }
}
