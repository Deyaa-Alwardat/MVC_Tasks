
namespace UniversityCafeteriaSupply.Models
{
    public class ConfirmedSupplyRequest
    {
        public int RequestId { get; set; }

        public List<SupplyRequestItem> Products { get; set; }

        public decimal Subtotal { get; set; }

        public decimal PreparationFee { get; set; }

        public decimal Discount { get; set; }

        public decimal FinalTotal { get; set; }

        public string Status { get; set; }
    }
}