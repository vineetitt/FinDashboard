using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinDashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly FinDashboardDbContext finDashboardDbContext;
        private readonly IPortfolioRepository portfolioRepository;

        public PortfolioController(FinDashboardDbContext finDashboardDbContext, IPortfolioRepository portfolioRepository)
        {
            this.finDashboardDbContext = finDashboardDbContext;
            this.portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        public IActionResult GetPortfolioByUserId(int userId)
        {
            try
            {
                var getPortfolio = portfolioRepository.GetPortfolioByUserId(userId);
                return Ok(getPortfolio);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public IActionResult UpdatePortfolioByUserId(int userId, int investedValue)
        {
            try
            {
                var isPortfolioUpdated = portfolioRepository.UpdatePortfolioByUserId(userId, investedValue);
                return Ok("Updated");
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
