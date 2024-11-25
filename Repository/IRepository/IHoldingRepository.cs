using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs;
using FinDashboard.API.Models.DTOs.HoldingDto;

namespace FinDashboard.API.Repository.IRepository
{
    public interface IHoldingRepository
    {
        void BuyStock(AddHoldingDto addHoldingDto);
        bool SellUserStock(AddHoldingDto addHoldingDto);
    }
}
