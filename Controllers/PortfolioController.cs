using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Repository;
using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinDashboard.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IUnitOfWorkRepository unitOfWorkRepository;

        public PortfolioController(IUnitOfWorkRepository unitOfWorkRepository)
        {
            this.unitOfWorkRepository = unitOfWorkRepository;
        }

        /// <summary>
        /// Retrieves a portfolio by the specified user ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Policy = "AdminPolicy")]
        [Authorize]
        public IActionResult GetPortfolioByUserId(int userId)
        {
            try
            {
                var getPortfolio = unitOfWorkRepository.PortfolioRepository.GetPortfolioByUserId(userId);
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

        /// <summary>
        /// Updates the portfolio by specified user ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="investedValue"></param>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult UpdatePortfolioByUserId(int userId, int investedValue)
        {
            try
            {
                var isPortfolioUpdated = unitOfWorkRepository.PortfolioRepository.UpdatePortfolioByUserId(userId, investedValue);
                return Ok("Updated");
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
        /// Adds a portfolio by the specified user ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddPortfolioByUserId(int userId)
        {
            try
            {
                var isPortfolioCreated = unitOfWorkRepository.PortfolioRepository.AddPortfolioByUserId(userId);
                return Ok("added");
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
