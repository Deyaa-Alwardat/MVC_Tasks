namespace NEXUS_Codefirst_Task.Models
{
    public class Review
    {
        public int Id { get; set; }
        public byte Rating { get; set; }
        public string Comment { get; set; }

        // Many side: التقييم يكتبه مستخدم واحد
        public int AuthorId { get; set; }
        public User Author { get; set; }

        // Many side: التقييم يخص حجز واحد
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}
