namespace AcademyGraduationSystem.Models
{
    public class SupplyRequestItem
    {
        public int SupplyRequestItemId { get; set; }

        public int SupplyRequestId { get; set; }

        public SupplyRequest SupplyRequest { get; set; }

        public int OfferId { get; set; }

        public Offer Offer { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
