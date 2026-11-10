namespace NEXUS_Codefirst_Task.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalFee { get; set; }

        // Many side: الحجز يعود لمستخدم واحد
        public int UserId { get; set; }
        public User User { get; set; }

        // Many side: الحجز يعود لمساحة واحدة
        public int SpaceId { get; set; }
        public Space Space { get; set; }

        // One side: الحجز ممكن يكون عليه أكثر من تقييم
        public ICollection<Review> Reviews { get; set; }
    }
}
