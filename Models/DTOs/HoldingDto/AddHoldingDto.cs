using FinDashboard.API.Models.Domain;

namespace FinDashboard.API.Models.DTOs
{
    public class AddHoldingDto
    {
        public int UserId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }

    }
}
