namespace FinDashboard.API.Models.Domain
{
    public class Asset
    {
        public int AssetID { get; set; }  
        public string AssetName { get; set; } = string.Empty;
        public decimal Quantity { get; set; } 
        public decimal CurrentPrice { get; set; }
        public decimal HighPrice { get; set; } 
        public decimal LowPrice { get; set; } 
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }

        public int PortfolioID { get; set; }

        public virtual Portfolio Portfolio { get; set; }
    }
}
