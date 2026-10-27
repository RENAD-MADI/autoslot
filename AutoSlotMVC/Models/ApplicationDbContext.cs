using Microsoft.EntityFrameworkCore;

namespace AutoSlotMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            // One car per service slot: the same date and time cannot be booked twice.
            modelBuilder.Entity<Booking>().HasIndex(b => new { b.BookingDate, b.BookingTime }).IsUnique();
        }
    }
}
