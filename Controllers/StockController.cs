using FinDashboard.API.Models.DTOs.AssetDto;
using FinDashboard.API.Models.DTOs.StockDto;
using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinDashboard.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class StockController : ControllerBase
    {
        private readonly IUnitOfWorkRepository unitOfWorkRepository;

        public StockController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            this.unitOfWorkRepository = unitOfWorkRepository;
        }

        /// <summary>
        /// Adds a new stock to the stocks list
        /// </summary>
        /// <param name="addStockDto"></param>
        /// <returns></returns>
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddStock(AddStockDto addStockDto)
        {
            try
            {
                var isAssetAdded = await unitOfWorkRepository.StockRepository.AddStock(addStockDto);
                return Ok(isAssetAdded);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Updates the details of an existing stock in the stocks list.
        /// </summary>
        /// <param name="stockId"></param>
        /// <param name="updateStockDto"></param>
        /// <returns></returns>
        [Authorize(Roles ="Admin")]
        [HttpPatch]
        public IActionResult UpdateStock(int stockId, UpdateStockDto updateStockDto)
        {
            try
            {
                var isUpdatedStock = unitOfWorkRepository.StockRepository.UpdateStock(stockId, updateStockDto);
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

        /// <summary>
        /// Deletes a stock from the stock list
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        [Authorize(Roles ="Admin")]
        [HttpDelete("{stockId}")]
        public IActionResult DeleteStock(int stockId)
         {
            try
            {
                var isStockDeleted = unitOfWorkRepository.StockRepository.DeleteStock(stockId);
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

        /// <summary>
        /// Retrieves a list of all stocks from stock list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllStock()
        {
            try
            {
                var getStocks = unitOfWorkRepository.StockRepository.GetAllStock();
                return Ok(getStocks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
