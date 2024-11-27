using FinDashboard.API.Models.DTOs;
using FinDashboard.API.Models.DTOs.HoldingDto;
using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinDashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoldingController : ControllerBase
    {
        private readonly IHoldingRepository holdingRepository;

        public HoldingController(IHoldingRepository holdingRepository)
        {
            this.holdingRepository = holdingRepository;
        }

        /// <summary>
        /// Initiates a purchase of stock for a specified user. 
        /// </summary>
        /// <param name="addHoldingDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddStock(AddHoldingDto addHoldingDto)
        {
            try
            {
                holdingRepository.BuyStock(addHoldingDto);
                return Ok();
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
        /// Initiates a sale of stock for a specified user.
        /// </summary>
        /// <param name="addHoldingDto"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult SellUserStock(AddHoldingDto addHoldingDto)
        {
            try
            {
                var isStockSold = holdingRepository.SellUserStock(addHoldingDto);
                return Ok("Stock Sold");
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

    }
}
