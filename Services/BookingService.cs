// Services/BookingService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;

namespace TH_WEB.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        
        public BookingService(ApplicationDbContext context)
        {
            _context = context;
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
                booking.Status = "Cancelled";
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
            return await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId)
        {
            return await _context.Bookings.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task CancelBookingAsync(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            if (booking != null)
            {
                booking.Status = "Cancelled";
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> SendBookingConfirmationEmailAsync(int bookingId)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.Hotel)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
            if (booking == null)
            {
                return false;
            }
            // Simulate sending email
            return true;
        }

        public async Task<bool> CheckRoomAvailabilityAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var overlappingBookings = await _context.Bookings
                .Where(b => b.RoomId == roomId)
                .Where(b => b.Status != "Cancelled")
                .Where(b =>
                    (checkIn >= b.CheckIn && checkIn < b.CheckOut) ||
                    (checkOut > b.CheckIn && checkOut <= b.CheckOut) ||
                    (checkIn <= b.CheckIn && checkOut >= b.CheckOut))
                .AnyAsync();
            return !overlappingBookings;
        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
            return booking;
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

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var overlappingBookings = await _context.Bookings
                .Where(b => b.RoomId == roomId && b.Status != "Cancelled")
                .Where(b =>
                    (checkIn >= b.CheckInDate && checkIn < b.CheckOutDate) ||
                    (checkOut > b.CheckInDate && checkOut <= b.CheckOutDate) ||
                    (checkIn <= b.CheckInDate && checkOut >= b.CheckOutDate))
                .AnyAsync();
            return !overlappingBookings;
        }

        public async Task<decimal> CalculateTotalPriceAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            if (room == null) return 0;
            int nights = (int)(checkOut - checkIn).TotalDays;
            return room.PricePerNight * nights;
        }

        public async Task SendBookingConfirmationEmailAsync(Booking booking)
        {
            // Simulate sending email
            await Task.CompletedTask;
        }
    }
}