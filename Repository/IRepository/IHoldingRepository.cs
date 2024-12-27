using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs;
using FinDashboard.API.Models.DTOs.HoldingDto;

namespace FinDashboard.API.Repository.IRepository
{
    public interface IHoldingRepository
    {
        Task BuyStock(AddHoldingDto addHoldingDto);
        Task<bool> SellUserStock(AddHoldingDto addHoldingDto);
        List<Holding> GetAllHolding();
    }
}
