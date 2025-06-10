// Controllers/HotelController.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;
using TH_WEB.Attributes;
using TH_WEB.Services.Authorization;
using TH_WEB.Extensions;

namespace TH_WEB.Controllers
{    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPermissionService _permissionService;
        
        public HotelController(ApplicationDbContext context, IPermissionService permissionService)
        {
            _context = context;
            _permissionService = permissionService;
        }
        
        // Public access for viewing hotels
        public async Task<IActionResult> Index(string city = "", DateTime? checkIn = null, DateTime? checkOut = null, int guests = 1)
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
        
        // Public access for viewing hotel details
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
        }        // Admin only - Create hotel
        [Authorize]
        [RequirePermission("hotel.create", "hotel.manage")]
        public IActionResult Create()
        {
            return View();
        }        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [RequirePermission("hotel.create", "hotel.manage")]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                hotel.CreatedAt = DateTime.UtcNow;
                hotel.UpdatedAt = DateTime.UtcNow;
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
                
                // Assign ownership to the creating user
                await _permissionService.AssignOwnershipAsync(User, "hotel", hotel.Id);
                
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }        // Admin or owner only - Edit hotel
        [Authorize]
        [RequirePermission("hotel.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            // Check resource access
            if (!await _permissionService.CanAccessResourceAsync(User, "hotel.edit", "hotel", id))
            {
                HttpContext.Items["AttemptedAction"] = "Hotel/Edit";
                HttpContext.Items["AttemptedResource"] = "hotel";
                HttpContext.Items["AttemptedResourceId"] = id;
                return View("~/Views/Shared/AccessDenied.cshtml");
            }

            return View(hotel);
        }[HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [RequirePermission("hotel.edit")]
        public async Task<IActionResult> Edit(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return NotFound();
            }            // Check resource access
            if (!await _permissionService.CanAccessResourceAsync(User, "hotel.edit", "hotel", id))
            {
                HttpContext.Items["AttemptedAction"] = "Hotel/Edit";
                HttpContext.Items["AttemptedResource"] = "hotel";
                HttpContext.Items["AttemptedResourceId"] = id;
                return View("~/Views/Shared/AccessDenied.cshtml");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    hotel.UpdatedAt = DateTime.UtcNow;
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }        // Admin only - Delete hotel
        [Authorize]
        [RequirePermission("hotel.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            // Check resource access
            if (!await _permissionService.CanAccessResourceAsync(User, "hotel.delete", "hotel", id))
            {
                HttpContext.Items["AttemptedAction"] = "Hotel/Delete";
                HttpContext.Items["AttemptedResource"] = "hotel";
                HttpContext.Items["AttemptedResourceId"] = id;
                return View("~/Views/Shared/AccessDenied.cshtml");
            }

            return View(hotel);
        }        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        [RequirePermission("hotel.delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check resource access
            if (!await _permissionService.CanAccessResourceAsync(User, "hotel.delete", "hotel", id))
            {
                HttpContext.Items["AttemptedAction"] = "Hotel/Delete";
                HttpContext.Items["AttemptedResource"] = "hotel";
                HttpContext.Items["AttemptedResourceId"] = id;
                return View("~/Views/Shared/AccessDenied.cshtml");
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }
    }
}