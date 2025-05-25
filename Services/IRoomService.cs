using System.Collections.Generic;
using System.Threading.Tasks;
using TH_WEB.Models;

namespace TH_WEB.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
        Task<Room> GetRoomByIdAsync(int id);
        Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId);
        Task<IEnumerable<Room>> SearchRoomsAsync(string searchTerm);
        Task<bool> CheckRoomAvailabilityAsync(int roomId, DateTime checkIn, DateTime checkOut);
    }
} 