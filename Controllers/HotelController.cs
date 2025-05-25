// Controllers/HotelController.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;

namespace TH_WEB.Controllers
{
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index(string city = null, DateTime? checkIn = null, DateTime? checkOut = null, int guests = 1)
        {
            var query = _context.Hotels.AsQueryable();
            
            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(h => h.City.Contains(city));
            }
            
            var hotels = await query
                .Include(h => h.Reviews)
                .ToListAsync();
                
            ViewBag.CheckIn = checkIn ?? DateTime.Now.AddDays(1);
            ViewBag.CheckOut = checkOut ?? DateTime.Now.AddDays(2);
            ViewBag.Guests = guests;
            
            return View(hotels);
        }
        
        public async Task<IActionResult> Details(int id, DateTime? checkIn = null, DateTime? checkOut = null, int guests = 1)
        {
            var hotel = await _context.Hotels
                .Include(h => h.Rooms)
                .Include(h => h.Reviews)
                .FirstOrDefaultAsync(h => h.Id == id);
                
            if (hotel == null)
            {
                return NotFound();
            }
            
            ViewBag.CheckIn = checkIn ?? DateTime.Now.AddDays(1);
            ViewBag.CheckOut = checkOut ?? DateTime.Now.AddDays(2);
            ViewBag.Guests = guests;
            
            return View(hotel);
        }
    }
}