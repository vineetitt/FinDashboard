using FinDashboard.API.Models.DTOs.AssetDto;
using Microsoft.AspNetCore.Mvc;

namespace FinDashboard.API.Repository.IRepository
{
    public interface IAssetRepository
    {
        bool AddAsset(AddAssetDto addAssetDto);
    }
}
