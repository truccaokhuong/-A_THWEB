using Microsoft.AspNetCore.Mvc;
using TH_WEB.Models;
using TH_WEB.Services;
using System.Threading.Tasks;

namespace TH_WEB.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<IActionResult> Index(int hotelId)
        {
            var rooms = await _roomService.GetRoomsByHotelIdAsync(hotelId);
            return View(rooms);
        }

        public async Task<IActionResult> Details(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> CheckAvailability(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var isAvailable = await _roomService.CheckRoomAvailabilityAsync(roomId, checkIn, checkOut);
            return Json(new { isAvailable });
        }
    }
} 