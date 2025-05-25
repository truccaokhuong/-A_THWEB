using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;

namespace TH_WEB.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _context;

        public RoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync()
        {
            return await _context.Rooms
                .Where(r => r.IsAvailable)
                .Include(r => r.Hotel)
                .OrderByDescending(r => r.Hotel.StarRating)
                .Take(12)
                .ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId)
        {
            return await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .OrderBy(r => r.PricePerNight)
                .ToListAsync();
        }

        public async Task<IEnumerable<Room>> SearchRoomsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAvailableRoomsAsync();

            return await _context.Rooms
                .Where(r => r.Name.Contains(searchTerm) || 
                           r.Description.Contains(searchTerm) ||
                           r.Hotel.Name.Contains(searchTerm))
                .Include(r => r.Hotel)
                .OrderByDescending(r => r.Hotel.StarRating)
                .ToListAsync();
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _context.Rooms
                .Include(r => r.Hotel)
                .OrderBy(r => r.Hotel.Name)
                .ThenBy(r => r.Name)
                .ToList();
        }

        public Room GetRoomById(int id)
        {
            return _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Room> GetRoomsByHotel(int hotelId)
        {
            return _context.Rooms
                .Include(r => r.Hotel)
                .Where(r => r.HotelId == hotelId)
                .OrderBy(r => r.Name)
                .ToList();
        }

        public IEnumerable<Room> GetAvailableRooms(int hotelId, DateTime checkIn, DateTime checkOut)
        {
            return _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.Bookings)
                .Where(r => r.HotelId == hotelId && r.IsAvailable &&
                    !r.Bookings.Any(b =>
                        (checkIn >= b.CheckInDate && checkIn < b.CheckOutDate) ||
                        (checkOut > b.CheckInDate && checkOut <= b.CheckOutDate) ||
                        (checkIn <= b.CheckInDate && checkOut >= b.CheckOutDate)))
                .OrderBy(r => r.Name)
                .ToList();
        }

        public bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = _context.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.Id == roomId);

            if (room == null || !room.IsAvailable)
                return false;

            return !room.Bookings.Any(b =>
                (checkIn >= b.CheckInDate && checkIn < b.CheckOutDate) ||
                (checkOut > b.CheckInDate && checkOut <= b.CheckOutDate) ||
                (checkIn <= b.CheckInDate && checkOut >= b.CheckOutDate));
        }

        public async Task<bool> CheckRoomAvailabilityAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = await _context.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null) return false;

            // Check if there are any overlapping bookings
            var hasOverlappingBooking = room.Bookings.Any(b =>
                b.Status != "Cancelled" &&
                ((checkIn >= b.CheckInDate && checkIn < b.CheckOutDate) ||
                 (checkOut > b.CheckInDate && checkOut <= b.CheckOutDate) ||
                 (checkIn <= b.CheckInDate && checkOut >= b.CheckOutDate)));

            return !hasOverlappingBooking;
        }
    }
} 