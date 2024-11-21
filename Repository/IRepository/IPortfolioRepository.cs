using FinDashboard.API.Models.Domain;

namespace FinDashboard.API.Repository.IRepository
{
    public interface IPortfolioRepository
    {
        Portfolio GetPortfolioByUserId(int userId);
        bool UpdatePortfolioByUserId(int userId, int investedValue);
        bool AddPortfolioByUserId(int userId);

    }
}
