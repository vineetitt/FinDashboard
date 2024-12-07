using FinDashboard.API.Models.DTOs;
using FinDashboard.API.Models.DTOs.HoldingDto;
using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult SellUserStock(AddHoldingDto addHoldingDto)
        {
            try
            {
                var isStockSold = unitOfWorkRepository.HoldingRepository.SellUserStock(addHoldingDto);
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
