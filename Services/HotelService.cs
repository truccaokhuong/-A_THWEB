using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;

namespace TH_WEB.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext _context;

        public HotelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetFeaturedHotelsAsync()
        {
            return await _context.Hotels
                .Where(h => h.StarRating >= 4)
                .OrderByDescending(h => h.StarRating)
                .Take(6)
                .ToListAsync();
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .Include(h => h.Reviews)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .Include(h => h.Reviews)
                .ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> SearchHotelsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllHotelsAsync();

            return await _context.Hotels
                .Where(h => h.Name.Contains(searchTerm) || 
                           h.City.Contains(searchTerm) || 
                           h.Country.Contains(searchTerm))
                .OrderByDescending(h => h.StarRating)
                .ToListAsync();
        }

        public IEnumerable<Hotel> GetAllHotels()
        {
            return _context.Hotels
                .Include(h => h.Reviews)
                .Include(h => h.Rooms)
                .OrderBy(h => h.Name)
                .ToList();
        }

        public Hotel GetHotelById(int id)
        {
            return _context.Hotels
                .Include(h => h.Reviews)
                .Include(h => h.Rooms)
                .FirstOrDefault(h => h.Id == id);
        }

        public IEnumerable<Hotel> GetTopRatedHotels(int count)
        {
            return _context.Hotels
                .Include(h => h.Reviews)
                .Include(h => h.Rooms)
                .OrderByDescending(h => h.Reviews.Average(r => r.Rating))
                .Take(count)
                .ToList();
        }

        public IEnumerable<Review> GetHotelReviews(int hotelId)
        {
            return _context.Reviews
                .Where(r => r.HotelId == hotelId)
                .OrderByDescending(r => r.ReviewDate)
                .ToList();
        }

        public double GetAverageRating(int hotelId)
        {
            var reviews = _context.Reviews.Where(r => r.HotelId == hotelId);
            if (!reviews.Any())
                return 0;
            return (double)reviews.Average(r => r.Rating);
        }

        public async Task AddReviewAsync(Review review)
        {
            review.ReviewDate = DateTime.Now;
            review.CreatedAt = DateTime.Now;
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }
    }
} 