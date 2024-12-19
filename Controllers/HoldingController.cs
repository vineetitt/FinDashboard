using FinDashboard.API.Models.DTOs;
using FinDashboard.API.Models.DTOs.HoldingDto;
using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinDashboard.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HoldingController : ControllerBase
    {
        //private readonly IHoldingRepository holdingRepository;
        private readonly IUnitOfWorkRepository unitOfWorkRepository;

        public HoldingController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            //this.holdingRepository = holdingRepository;
            this.unitOfWorkRepository = unitOfWorkRepository;
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
                unitOfWorkRepository.HoldingRepository.BuyStock(addHoldingDto);
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
        public async Task<IActionResult> SellUserStock(AddHoldingDto addHoldingDto)
        {
            try
            {
                var isStockSold = await unitOfWorkRepository.HoldingRepository.SellUserStock(addHoldingDto);
                if (isStockSold!=null)
                {
                    await unitOfWorkRepository.CompleteAsync();
                    return Ok("Stock Sold");
                }
                return BadRequest("Failed to sell stock.");

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
