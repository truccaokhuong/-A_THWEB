// Controllers/BookingController.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;
using TH_WEB.Services;

namespace TH_WEB.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookingService _bookingService;
        
        public BookingController(ApplicationDbContext context, IBookingService bookingService)
        {
            _context = context;
            _bookingService = bookingService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Create(int roomId, DateTime checkIn, DateTime checkOut, int guests)
        {
            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == roomId);
                
            if (room == null)
            {
                return NotFound();
            }
            
            int nights = (int)(checkOut - checkIn).TotalDays;
            decimal totalPrice = room.PricePerNight * nights;
            
            var booking = new Booking
            {
                RoomId = roomId,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                NumberOfGuests = guests,
                NumberOfRooms = 1,
                TotalPrice = totalPrice,
                CreatedAt = DateTime.UtcNow,
                Status = BookingStatus.Pending.ToString()
            };
            
            ViewBag.Room = room;
            ViewBag.Nights = nights;
            
            return View(booking);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                // Lấy lại thông tin phòng và số đêm để truyền vào ViewBag
                var room = await _context.Rooms.Include(r => r.Hotel).FirstOrDefaultAsync(r => r.Id == booking.RoomId);
                if (room == null)
                {
                    ModelState.AddModelError(string.Empty, "Room not found.");
                    ViewBag.Room = null;
                    ViewBag.Nights = 0;
                }
                else
                {
                    ViewBag.Room = room;
                    ViewBag.Nights = (booking.CheckOutDate - booking.CheckInDate).Days;
                }
                return View(booking);
            }

            booking.Status = BookingStatus.Pending.ToString();
            booking.CreatedAt = DateTime.UtcNow;
            booking.UpdatedAt = DateTime.UtcNow;

            var result = await _bookingService.CreateBookingAsync(booking);
            if (result != null)
            {
                await _bookingService.SendBookingConfirmationEmailAsync(result);
                return RedirectToAction("Confirmation", new { id = result.Id });
            }

            return View(booking);
        }
        
        public async Task<IActionResult> Confirmation(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        public async Task<IActionResult> MyBookings()
        {
            var userId = User.Identity.Name; // Assuming you're using ASP.NET Core Identity
            var bookings = await _bookingService.GetUserBookingsAsync(userId);
            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            booking.Status = BookingStatus.Cancelled.ToString();
            booking.UpdatedAt = DateTime.UtcNow;

            await _bookingService.UpdateBookingAsync(booking);
            return RedirectToAction("MyBookings");
        }
    }
}