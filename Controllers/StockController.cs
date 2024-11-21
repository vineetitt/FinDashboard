using FinDashboard.API.Models.DTOs.AssetDto;
using FinDashboard.API.Models.DTOs.StockDto;
using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinDashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            this.stockRepository = stockRepository;
        }

        [HttpPost]
        public IActionResult AddStock(AddStockDto addStockDto)
        {
            try
            {
                var isAssetAdded = stockRepository.AddStock(addStockDto);
                return Ok("Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch]
        public IActionResult UpdateStock(int stockId, UpdateStockDto updateStockDto)
        {
            try
            {
                var isUpdatedStock = stockRepository.UpdateStock(stockId, updateStockDto);
                return Ok("updated");
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

        [HttpDelete]
        public IActionResult DeleteStock(int stockId)
        {
            try
            {
                var isStockDeleted = stockRepository.DeleteStock(stockId);
                return Ok("Deleted");
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

        [HttpGet]
        public IActionResult GetAllStock()
        {
            try
            {
                var getStocks = stockRepository.GetAllStock();
                return Ok(getStocks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
