namespace Task8.Models
{
    public class SupplyRequest
    {
        public int Id { get; set; }

        public DateTime RequestDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        public string UserId { get; set; }
        public ICollection<RequestItem> RequestItems { get; set; } = new List<RequestItem>();
    }
}
