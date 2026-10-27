using System.ComponentModel.DataAnnotations;

namespace AutoSlotMVC.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } = "";

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; } = "";

        [Required, Phone, StringLength(20)]
        public string Phone { get; set; } = "";

        // Stores the ASP.NET Core password hash, never the plain password.
        [Required, StringLength(200, MinimumLength = 6)]
        public string Password { get; set; } = "";

        public List<Booking> Bookings { get; set; } = new();
    }
}
