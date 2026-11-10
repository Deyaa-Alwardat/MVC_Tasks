namespace NEXUS_Codefirst_Task.Models
{
    public class Space
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public decimal DailyPrice { get; set; }

        // Many side: المساحة يملكها مستخدم واحد (المالك)
        public int OwnerId { get; set; }
        public User Owner { get; set; }

        // One side: المساحة لها أكثر من حجز
        public ICollection<Booking> Bookings { get; set; }
    }
}
