namespace AcademyGraduationSystem.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Offer> Offers { get; set; } = new List<Offer>();
    }
}
