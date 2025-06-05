// Services/BookingService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;
using TH_WEB.Services;

namespace TH_WEB.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        
        public BookingService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _context.Bookings.ToList();
        }

        public Booking GetBookingById(int id)
        {
            return _context.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Booking> GetBookingsByRoom(int roomId)
        {
            return _context.Bookings.Where(b => b.RoomId == roomId).ToList();
        }

        public IEnumerable<Booking> GetBookingsByHotel(int hotelId)
        {
            return _context.Bookings.Include(b => b.Room).Where(b => b.Room.HotelId == hotelId).ToList();
        }

        public Booking CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return booking;
        }

        public void UpdateBooking(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
        }

        public void CancelBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking != null)
            {
                booking.Status = BookingStatus.Cancelled;
                _context.SaveChanges();
            }
        }

        public decimal CalculateTotalPrice(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null) return 0;
            int nights = (int)(checkOut - checkIn).TotalDays;
            return room.PricePerNight * nights;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId)
        {
            return await _context.Bookings
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            // Validate room availability
            var isAvailable = await IsRoomAvailableAsync(booking.RoomId, booking.CheckInDate, booking.CheckOutDate);
            if (!isAvailable)
            {
                return null;
            }

            booking.Status = BookingStatus.Pending;
            booking.CreatedAt = DateTime.UtcNow;
            booking.UpdatedAt = DateTime.UtcNow;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var existingBookings = await _context.Bookings
                .Where(b => b.RoomId == roomId &&
                           b.Status != BookingStatus.Cancelled &&
                           ((checkIn >= b.CheckInDate && checkIn < b.CheckOutDate) ||
                            (checkOut > b.CheckInDate && checkOut <= b.CheckOutDate) ||
                            (checkIn <= b.CheckInDate && checkOut >= b.CheckOutDate)))
                .AnyAsync();

            return !existingBookings;
        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            try
            {
                booking.UpdatedAt = DateTime.UtcNow;
                _context.Bookings.Update(booking);
                await _context.SaveChangesAsync();
                return booking;
            }
            catch
            {
                return null;
            }
        }

        public async Task CancelBookingAsync(int id)
        {
            var booking = await GetBookingByIdAsync(id);
            if (booking == null)
            {
                return;
            }

            booking.Status = BookingStatus.Cancelled;
            booking.UpdatedAt = DateTime.UtcNow;

            await UpdateBookingAsync(booking);
        }

        public async Task SendBookingConfirmationEmailAsync(Booking booking)
        {
            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == booking.RoomId);

            if (room == null)
            {
                return;
            }

            var body = $"Thank you for your booking with EzBooking!\n\nBooking Details:\nHotel: {room.Hotel.Name}\nRoom Type: {room.RoomType}\nCheck-in Date: {booking.CheckInDate.ToShortDateString()}\nCheck-out Date: {booking.CheckOutDate.ToShortDateString()}\nNumber of Adults: {booking.NumberOfAdults}\nNumber of Children: {booking.NumberOfChildren}\nNumber of Infants: {booking.NumberOfInfants}\nTotal Price: ${booking.TotalPrice:F2}\nBooking Reference: {booking.BookingReference}\n\nWe look forward to welcoming you!\nEzBooking Team";

            await _emailService.SendEmailAsync(booking.GuestEmail, "EzBooking - Booking Confirmation", body);
        }

        public async Task<bool> ValidateBookingAsync(Booking booking)
        {
            if (booking.CheckInDate >= booking.CheckOutDate)
            {
                return false;
            }

            var totalGuests = booking.NumberOfAdults + booking.NumberOfChildren + booking.NumberOfInfants;
            if (totalGuests <= 0)
            {
                return false;
            }

            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == booking.RoomId);

            if (room == null)
            {
                return false;
            }

            if (totalGuests > room.MaxOccupancy)
            {
                return false;
            }

            return await IsRoomAvailableAsync(booking.RoomId, booking.CheckInDate, booking.CheckOutDate);
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> CalculateTotalPriceAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            if (room == null) return 0;
            int nights = (int)(checkOut - checkIn).TotalDays;
            return room.PricePerNight * nights;
        }

        private string GenerateBookingConfirmationEmailBody(Booking booking)
        {
            // Construct the email body with booking details
            var body = $"Thank you for your booking with EzBooking!\n\nBooking Details:\nHotel: {booking.Room.Hotel.Name}\nRoom Type: {booking.Room.RoomType}\nCheck-in Date: {booking.CheckInDate.ToShortDateString()}\nCheck-out Date: {booking.CheckOutDate.ToShortDateString()}\nNumber of Adults: {booking.NumberOfAdults}\nNumber of Children: {booking.NumberOfChildren}\nNumber of Infants: {booking.NumberOfInfants}\nTotal Price: ${booking.TotalPrice:F2}\nBooking Reference: {booking.BookingReference}\n\nWe look forward to welcoming you!\nEzBooking Team";

            return body;
        }
    }
}