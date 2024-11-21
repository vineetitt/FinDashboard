using FinDashboard.API.Models.DTOs;
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
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
