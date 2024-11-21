namespace FinDashboard.API.Models.Domain
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        public int UserId { get; set; }
        public decimal CurrentValue { get; set; } = 0;
        public decimal InvestedValue { get; set; } = 0;
        public decimal ProfitLoss => CurrentValue - InvestedValue;
        public virtual User User { get; set; }
        public virtual ICollection<Holding> Holdings { get; set; } = new List<Holding>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    }
}
