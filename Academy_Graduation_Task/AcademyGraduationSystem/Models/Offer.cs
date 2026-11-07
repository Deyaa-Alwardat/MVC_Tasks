namespace AcademyGraduationSystem.Models
{
    public class Offer
    {
        public int OfferId { get; set; }

        public string OfferCode { get; set; }

        public string OfferName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public List<SupplyRequestItem> SupplyRequestItems { get; set; } = new List<SupplyRequestItem>();
    }
}
