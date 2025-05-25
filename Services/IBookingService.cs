using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TH_WEB.Models;

namespace TH_WEB.Services
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetAllBookings();
        Booking GetBookingById(int id);
        IEnumerable<Booking> GetBookingsByRoom(int roomId);
        IEnumerable<Booking> GetBookingsByHotel(int hotelId);
        Booking CreateBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void CancelBooking(int id);
        decimal CalculateTotalPrice(int roomId, DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking> UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int id);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut);
        Task<decimal> CalculateTotalPriceAsync(int roomId, DateTime checkIn, DateTime checkOut);
        Task SendBookingConfirmationEmailAsync(Booking booking);
        Task CancelBookingAsync(int id);
    }
} 