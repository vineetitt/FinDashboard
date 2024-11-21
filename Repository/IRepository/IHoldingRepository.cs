using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs;

namespace FinDashboard.API.Repository.IRepository
{
    public interface IHoldingRepository
    {
        void BuyStock(AddHoldingDto addHoldingDto);
    }
}
