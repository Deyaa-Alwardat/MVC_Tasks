namespace NEXUS_Codefirst_Task.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        // Many side: علاقة المستخدم بالشركة
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        // One side: المستخدم له أكثر من مساحة، وحجز، وتقييم
        public ICollection<Space> Spaces { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
