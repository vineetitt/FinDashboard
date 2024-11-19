using FinDashboard.API.Data;
using FinDashboard.API.Models.Domain;
using FinDashboard.API.Models.DTOs.AssetDto;
using FinDashboard.API.Repository.IRepository;

namespace FinDashboard.API.Repository
{
    public class AssetRepository:IAssetRepository
    {
        private readonly FinDashboardDbContext finDashboardDbContext;

        public AssetRepository(FinDashboardDbContext finDashboardDbContext)
        {
            this.finDashboardDbContext = finDashboardDbContext;
        }

        public bool AddAsset(AddAssetDto addAssetDto)
        {
            
            var asset = new Asset()
            {
                AssetName = addAssetDto.AssetName,
                Quantity = addAssetDto.Quantity,
            };
            var data = finDashboardDbContext.Assets.FirstOrDefault(asset => asset.AssetName == addAssetDto.AssetName);
            if (data != null)
            {
                data.Quantity = data.Quantity + asset.Quantity;
            }
            else
            {
                finDashboardDbContext.Assets.Add(asset);
            }
            finDashboardDbContext.SaveChanges();
            return true;

        }
    }
}
