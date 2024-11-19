using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Repository.IRepository;

namespace FinDashboard.API.Repository
{
    public class PortfolioRepository:IPortfolioRepository
    {
        private readonly FinDashboardDbContext finDashboardDbContext;

        public PortfolioRepository(FinDashboardDbContext finDashboardDbContext)
        {
            this.finDashboardDbContext = finDashboardDbContext;
        }

        public Portfolio GetPortfolioByUserId(int userId)
        {
            var getPortfolio = finDashboardDbContext.Portfolios.FirstOrDefault(portfolio => portfolio.UserId == userId);

            if (getPortfolio != null)
            {
                return getPortfolio;
            }
            else
            {
                throw new CustomException($"Portfolio with this id:{userId} does not exixts", 200);
            }
        }

        public bool UpdatePortfolioByUserId(int userId, int value)
        {
            var resultedPortfolio = GetPortfolioByUserId(userId);
            if (resultedPortfolio != null)
            {
                resultedPortfolio.InvestedValue = value;
                return true;
            }
            throw new CustomException($"Portfolio with this id:{userId} does not exixts", 200);
        }
    }
}
