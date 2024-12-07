using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinDashboard.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockPriceHistoryController : ControllerBase
    {
        private readonly IUnitOfWorkRepository unitOfWorkRepository;

        public StockPriceHistoryController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            this.unitOfWorkRepository = unitOfWorkRepository;
        }

        /// <summary>
        /// Retrieves the price history of a specific stock by its unique ID
        /// </summary>
        /// <param name="stockID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetStockPriceHistoryByStockID(int stockID, DateTime date)
        {
            try
            {
                var StockPrice = unitOfWorkRepository.StockPriceHistoryRepository.GetStockPriceHistoryByStockID(stockID, date);
                return Ok(StockPrice);
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
