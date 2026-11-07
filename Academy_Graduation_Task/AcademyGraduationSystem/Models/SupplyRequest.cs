namespace AcademyGraduationSystem.Models
{
    public class SupplyRequest
    {
        public int SupplyRequestId { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DiscountPercentage { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal FinalTotal { get; set; }

        public List<SupplyRequestItem> Items { get; set; } = new List<SupplyRequestItem>();
    }
}
