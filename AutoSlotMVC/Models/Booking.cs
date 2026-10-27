using System.ComponentModel.DataAnnotations;

namespace AutoSlotMVC.Models
{
    public class Booking
    {
        public int Id { get; set; }

        // Owner of the booking; each user only sees and changes their own bookings.
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } = "";

        [Required, Phone, StringLength(20)]
        public string Phone { get; set; } = "";

        [Required, StringLength(50)]
        public string CarSerial { get; set; } = "";

        [Required, StringLength(50)]
        public string CarModel { get; set; } = "";

        [Required, StringLength(50)]
        public string ServiceType { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan BookingTime { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
