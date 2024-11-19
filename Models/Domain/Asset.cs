namespace FinDashboard.API.Models.Domain
{
    public class Asset
    {
        public int AssetID { get; set; }  
        public string AssetName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal CurrentPrice { get; set; } = 0;
        public decimal HighPrice { get; set; } = 0;
        public decimal LowPrice { get; set; } = 0;
        public decimal OpenPrice { get; set; } = 0;
        public decimal ClosePrice { get; set; }

        public int PortfolioID { get; set; }

        public virtual Portfolio Portfolio { get; set; }
    }
}
