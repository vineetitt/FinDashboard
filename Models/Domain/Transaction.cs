using System.ComponentModel.DataAnnotations;

namespace FinDashboard.API.Models.Domain
{
    public class Transaction
    {
        [Key]
        public int TransactioID { get; set; }
        public DateTime TransactioDate { get; set; } = DateTime.Now;
        public string TransactionType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalAmount => Quantity * PricePerUnit;

        public int PortfolioID { get; set; }
        public virtual Portfolio Portfolio { get; set; }

        public int StockID { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
