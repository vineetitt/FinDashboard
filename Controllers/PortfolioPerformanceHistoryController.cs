using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinDashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioPerformanceHistoryController : ControllerBase
    {
        private readonly IUnitOfWorkRepository unitOfWorkRepository;

        public PortfolioPerformanceHistoryController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            this.unitOfWorkRepository = unitOfWorkRepository;
        }

        [HttpPost]
        public IActionResult AddPortfolioPerformancePrice(int portfolioId, int portfolioValue, int investedValue)
        {
            try
            {
                var response = unitOfWorkRepository.PortfolioPerformanceHistoryRepository.AddPortfolioPerformancePrice(portfolioId, portfolioValue, investedValue);
                return Ok("Added");
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message, ex.statusCode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet]

        public IActionResult GetPortfolioPerformancePriceHistory(int portfolioId, DateTime date)
        {
            try
            {
                var response = unitOfWorkRepository.PortfolioPerformanceHistoryRepository.GetPortfolioPerformancePriceHistory(portfolioId, date);
                return Ok(response);
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
