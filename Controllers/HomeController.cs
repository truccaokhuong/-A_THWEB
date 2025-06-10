// Controllers/HomeController.cs
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;
using ViewModels;
using System.Collections.Generic;

namespace TH_WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var popularHotels = await _context.Hotels
                .OrderByDescending(h => h.TotalBookings)
                .Take(5)
                .ToListAsync();
                
            var topRatedHotels = await _context.Hotels
                .OrderByDescending(h => h.Rating)
                .Take(5)
                .ToListAsync();

            var featuredHotels = await _context.Hotels
                .Where(h => h.IsFeatured)
                .Take(6)
                .ToListAsync();

            // Nếu không có featured hotels, lấy top rated hotels
            if (!featuredHotels.Any())
            {
                featuredHotels = await _context.Hotels
                    .OrderByDescending(h => h.Rating)
                    .Take(6)
                    .ToListAsync();
            }

            var recentBookings = await _context.Bookings
                .Include(b => b.Hotel)
                .Include(b => b.Room)
                .OrderByDescending(b => b.CreatedAt)
                .Take(5)
                .ToListAsync();            var activeOffers = await _context.Offers
                .Where(o => o.StartDate <= DateTime.Now && o.EndDate >= DateTime.Now && o.IsActive)
                .OrderByDescending(o => o.DiscountPercentage)
                .Take(3)
                .ToListAsync();

            var viewModel = new HomeViewModel
            {
                FeaturedHotels = featuredHotels,
                RecentBookings = recentBookings,
                PopularDestinations = popularHotels,
                Offers = activeOffers,
                TopDestinations = topRatedHotels,
                // Thêm các destination phổ biến
                QuickDestinations = GetQuickDestinations()
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Search(string destination, DateTime checkIn, DateTime checkOut, int guests = 2)
        {
            // Validate dates
            if (checkIn < DateTime.Now.Date)
            {
                checkIn = DateTime.Now.AddDays(1);
            }
            if (checkOut <= checkIn)
            {
                checkOut = checkIn.AddDays(1);
            }

            // Redirect to Hotel Index with search parameters
            return RedirectToAction("Index", "Hotel", new 
            { 
                city = destination, 
                checkIn = checkIn, 
                checkOut = checkOut, 
                guests = guests 
            });
        }

        public IActionResult SearchDestination(string city)
        {
            var tomorrow = DateTime.Now.AddDays(1);
            var dayAfter = DateTime.Now.AddDays(2);
            
            return RedirectToAction("Index", "Hotel", new 
            { 
                city = city,
                checkIn = tomorrow,
                checkOut = dayAfter,
                guests = 2
            });
        }

        // API endpoint for autocomplete
        [HttpGet]
        public IActionResult GetDestinations(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return Json(new List<object>());
            }            var destinations = _context.Hotels
                .Where(h => h.City.Contains(term) || h.Name.Contains(term))
                .Select(h => new { 
                    label = $"{h.City}, {h.Country}", 
                    value = h.City,
                    hotelCount = _context.Hotels.Count(x => x.City == h.City)
                })
                .Distinct()
                .OrderBy(h => h.label)
                .Take(10)
                .ToList();

            return Json(destinations);
        }

        // Lấy danh sách destination nhanh
        private List<QuickDestination> GetQuickDestinations()
        {
            return new List<QuickDestination>
            {
                new QuickDestination 
                { 
                    Name = "New York", 
                    Description = "The city that never sleeps",
                    ImageUrl = "/images/destinations/new-york.jpg"
                },
                new QuickDestination 
                { 
                    Name = "Miami", 
                    Description = "Sun, sand, and nightlife",
                    ImageUrl = "/images/destinations/miami.jpg"
                },
                new QuickDestination 
                { 
                    Name = "Los Angeles", 
                    Description = "City of angels",
                    ImageUrl = "/images/destinations/los-angeles.jpg"
                },
                new QuickDestination 
                { 
                    Name = "San Francisco", 
                    Description = "Golden Gate and more",
                    ImageUrl = "/images/destinations/san-francisco.jpg"
                },
                new QuickDestination 
                { 
                    Name = "Denver", 
                    Description = "Mile high city",
                    ImageUrl = "/images/destinations/denver.jpg"
                },
                new QuickDestination 
                { 
                    Name = "Chicago", 
                    Description = "The windy city",
                    ImageUrl = "/images/destinations/chicago.jpg"
                }
            };
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
    // Thêm class cho Quick Destinations
    public class QuickDestination
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}