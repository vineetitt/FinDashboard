namespace FinDashboard.API.Models.Domain
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        public int UserId { get; set; }
        public decimal CurrentValue { get; set; } = 0;
        public decimal InvestedValue { get; set; } = 0;

        public virtual User User { get; set; }
        public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    }
}
