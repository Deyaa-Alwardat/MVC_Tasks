namespace AcademyGraduationSystem.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public List<Supplier> Suppliers { get; set; } = new List<Supplier>();
    }
}
