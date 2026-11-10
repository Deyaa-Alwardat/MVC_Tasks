namespace NEXUS_Codefirst_Task.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public decimal MonthlyBudget { get; set; }

        // One side: الشركة الواحدة لها أكثر من مستخدم
        public ICollection<User> Users { get; set; }
    }
}
