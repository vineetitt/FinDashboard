using FinDashboard.API.Models.DTOs.AssetDto;
using FinDashboard.API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinDashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepository assetRepository;

        public AssetController(IAssetRepository assetRepository)
        {
            this.assetRepository = assetRepository;
        }

        [HttpPost]
        public IActionResult AddAsset(AddAssetDto addAssetDto)
        {
            try
            {
                var isAssetAdded = assetRepository.AddAsset(addAssetDto);
                return Ok("Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
