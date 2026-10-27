using AutoSlotMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoSlotMVC.Controllers
{
    public class HomeController : Controller
    {
        private const string SessionUserId = "UserId";
        private const string SessionUserName = "UserName";

        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _hasher = new();

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int? CurrentUserId => HttpContext.Session.GetInt32(SessionUserId);

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        // Used by the production exception handler ("/Home/Error"), which had no action.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // ---------- Accounts ----------

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            if (user != null && _hasher.VerifyHashedPassword(user, user.Password, password ?? "") != PasswordVerificationResult.Failed)
            {
                HttpContext.Session.SetInt32(SessionUserId, user.Id);
                HttpContext.Session.SetString(SessionUserName, user.FullName);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = "Invalid Email or Password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(User user)
        {
            if (_context.Users.Any(x => x.Email == user.Email))
            {
                ModelState.AddModelError(nameof(user.Email), "An account with this email already exists.");
            }
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            user.Password = _hasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction(nameof(Login));
        }

        // ---------- Bookings (signed-in users only, each sees their own) ----------

        public IActionResult Booking()
        {
            if (CurrentUserId == null) return RedirectToAction(nameof(Login));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Booking(Booking booking)
        {
            if (CurrentUserId is not int userId) return RedirectToAction(nameof(Login));

            ValidateSlot(booking);
            if (!ModelState.IsValid)
            {
                return View(booking);
            }

            booking.UserId = userId;
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            ModelState.Clear();
            ViewBag.Message = "Booking Added Successfully";
            return View();
        }

        public IActionResult ViewBookings()
        {
            if (CurrentUserId is not int userId) return RedirectToAction(nameof(Login));

            var bookings = _context.Bookings
                .Where(b => b.UserId == userId)
                .OrderBy(b => b.BookingDate).ThenBy(b => b.BookingTime)
                .ToList();

            return View(bookings);
        }

        public IActionResult EditBooking(int id)
        {
            var booking = FindOwnBooking(id);
            if (booking == null) return CurrentUserId == null ? RedirectToAction(nameof(Login)) : NotFound();
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBooking(Booking input)
        {
            var booking = FindOwnBooking(input.Id);
            if (booking == null) return CurrentUserId == null ? RedirectToAction(nameof(Login)) : NotFound();

            ValidateSlot(input);
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            booking.FullName = input.FullName;
            booking.Phone = input.Phone;
            booking.CarSerial = input.CarSerial;
            booking.CarModel = input.CarModel;
            booking.ServiceType = input.ServiceType;
            booking.BookingDate = input.BookingDate;
            booking.BookingTime = input.BookingTime;
            booking.Notes = input.Notes;
            _context.SaveChanges();

            return RedirectToAction(nameof(ViewBookings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBooking(int id)
        {
            var booking = FindOwnBooking(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(ViewBookings));
        }

        private Booking? FindOwnBooking(int id)
        {
            var userId = CurrentUserId;
            return userId == null ? null : _context.Bookings.FirstOrDefault(b => b.Id == id && b.UserId == userId);
        }

        // Bookings must be in the future and must not take a slot that is already booked.
        private void ValidateSlot(Booking booking)
        {
            if (booking.BookingDate.Date + booking.BookingTime < DateTime.Now)
            {
                ModelState.AddModelError(nameof(booking.BookingDate), "Please choose a date and time in the future.");
            }

            bool taken = _context.Bookings.AsNoTracking().Any(b =>
                b.Id != booking.Id &&
                b.BookingDate == booking.BookingDate.Date &&
                b.BookingTime == booking.BookingTime);
            if (taken)
            {
                ModelState.AddModelError(nameof(booking.BookingTime), "This time slot is already booked. Please choose another time.");
            }

            // Set by the server, not the form.
            ModelState.Remove(nameof(booking.UserId));
            ModelState.Remove(nameof(booking.User));
        }
    }
}
