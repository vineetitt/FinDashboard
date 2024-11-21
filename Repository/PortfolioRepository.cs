using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Repository.IRepository;

namespace FinDashboard.API.Repository
{
    public class PortfolioRepository : IPortfolioRepository
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

        public bool UpdatePortfolioByUserId(int userId, int investedValue)
        {
            var resultedPortfolio = GetPortfolioByUserId(userId);
            if (resultedPortfolio != null)
            {
                resultedPortfolio.InvestedValue = investedValue;
                return true;
            }
            throw new CustomException($"Portfolio with this id:{userId} does not exixts", 200);
        }  //Cuurent value can also be changed so apply that also

        public bool AddPortfolioByUserId(int userId)
        {
            if (userId == 0)
            {
                throw new CustomException("User Id cannot be null neither be empty", 400);
            }

            var user = finDashboardDbContext.Users.FirstOrDefault(user => user.UserID == userId);
            if (user != null)
            {
                var portfolio = new Portfolio()
                {
                    UserId = user.UserID,
                    InvestedValue = 0,
                    CurrentValue = 0
                };
                finDashboardDbContext.Add(portfolio);
                finDashboardDbContext.SaveChanges();
                return true;
            }
            throw new CustomException($"User with userId: {userId} not found", 200);
            
        }


    }
}
