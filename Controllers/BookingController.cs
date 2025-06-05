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
        public async Task<IActionResult> Create(int roomId, DateTime checkIn, DateTime checkOut, int adults, int children = 0, int infants = 0)
        {
            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == roomId);
                
            if (room == null)
            {
                return NotFound();
            }
            
            int nights = (int)(checkOut - checkIn).TotalDays;
            decimal totalPrice = room.Price * nights;
            
            var booking = new Booking
            {
                RoomId = roomId,
                HotelId = room.HotelId,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                NumberOfAdults = adults,
                NumberOfChildren = children,
                NumberOfInfants = infants,
                TotalPrice = totalPrice,
                CreatedAt = DateTime.UtcNow,
                Status = BookingStatus.Pending,
                GuestName = "",
                GuestEmail = "",
                GuestPhone = ""
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

            booking.CreatedAt = DateTime.UtcNow;
            booking.UpdatedAt = DateTime.UtcNow;
            
            if (booking.HotelId == null)
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == booking.RoomId);
                if (room != null) booking.HotelId = room.HotelId;
            }

            var result = await _bookingService.CreateBookingAsync(booking);
            if (result != null)
            {
                var userId = User.Identity?.Name;
                if (userId != null)
                {
                    result.UserId = userId;
                    await _bookingService.UpdateBookingAsync(result);
                }

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
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            
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

            booking.Status = BookingStatus.Cancelled;
            booking.UpdatedAt = DateTime.UtcNow;

            await _bookingService.UpdateBookingAsync(booking);
            return RedirectToAction("MyBookings");
        }
    }
}