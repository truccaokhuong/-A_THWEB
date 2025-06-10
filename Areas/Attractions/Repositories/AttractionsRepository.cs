using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Areas.Attractions.Models;
using TH_WEB.Areas.Attractions.ViewModels;
using TH_WEB.Data;

namespace TH_WEB.Areas.Attractions.Repositories
{
    public class AttractionsRepository : IAttractionsRepository
    {
        private readonly ApplicationDbContext _context;

        public AttractionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attraction>> GetAllAsync()
        {
            return await _context.Attractions
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task<Attraction> GetByIdAsync(int id)
        {
            var attraction = await _context.Attractions
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
            
            if (attraction == null)
            {
                throw new KeyNotFoundException($"Attraction with ID {id} not found.");
            }
            
            return attraction;
        }

        public async Task<Attraction> CreateAsync(Attraction attraction)
        {
            attraction.CreatedAt = DateTime.UtcNow;
            attraction.UpdatedAt = DateTime.UtcNow;
            
            _context.Attractions.Add(attraction);
            await _context.SaveChangesAsync();
            
            return attraction;
        }

        public async Task<Attraction> UpdateAsync(Attraction attraction)
        {
            attraction.UpdatedAt = DateTime.UtcNow;
            
            _context.Entry(attraction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return attraction;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attraction = await _context.Attractions
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
                
            if (attraction == null) return false;

            attraction.IsDeleted = true;
            attraction.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Attractions
                .AnyAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<IEnumerable<Attraction>> SearchAsync(string? name = null, AttractionCategory? category = null, AttractionStatus? status = null)
        {
            var query = _context.Attractions
                .Include(a => a.OperatingHours)
                .Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (category.HasValue)
            {
                query = query.Where(a => a.Category == category.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(a => a.Status == status.Value);
            }

            return await query.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<IEnumerable<Attraction>> GetByLocationAsync(string location)
        {
            return await _context.Attractions
                .Where(a => !a.IsDeleted && a.Location.Contains(location))
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attraction>> GetByCategoryAsync(AttractionCategory category)
        {
            return await _context.Attractions
                .Where(a => !a.IsDeleted && a.Category == category)
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attraction>> GetByStatusAsync(AttractionStatus status)
        {
            return await _context.Attractions
                .Where(a => !a.IsDeleted && a.Status == status)
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attraction>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Attractions
                .Where(a => !a.IsDeleted && a.Price >= minPrice && a.Price <= maxPrice)
                .OrderBy(a => a.Price)
                .ToListAsync();
        }        public async Task<IEnumerable<Attraction>> GetPopularAsync(int count = 10)
        {
            return await _context.Attractions
                .Where(a => !a.IsDeleted && a.Status == AttractionStatus.Active)
                .OrderByDescending(a => a.Rating)
                .ThenByDescending(a => a.ViewCount)
                .ThenBy(a => a.Name) // Add secondary sort for consistency
                .Take(count)
                .ToListAsync();
        }        public async Task<IEnumerable<Attraction>> GetFeaturedAsync(int count = 5)
        {
            return await _context.Attractions
                .Where(a => !a.IsDeleted && a.Status == AttractionStatus.Active && a.IsFeatured)
                .OrderByDescending(a => a.Rating)
                .ThenBy(a => a.Name) // Add secondary sort for consistency
                .Take(count)
                .ToListAsync();
        }        public async Task<IEnumerable<Attraction>> GetRecentAsync(int count = 10)
        {
            return await _context.Attractions
                .Where(a => !a.IsDeleted && a.Status == AttractionStatus.Active)
                .OrderByDescending(a => a.CreatedAt)
                .ThenBy(a => a.Name) // Add secondary sort for consistency
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attraction>> GetNearbyAsync(double latitude, double longitude, double radiusKm)
        {
            var attractions = await _context.Attractions
                .Where(a => !a.IsDeleted && a.Latitude.HasValue && a.Longitude.HasValue)
                .ToListAsync();

            return attractions
                .Where(a => CalculateDistance(
                    (double)a.Latitude!.Value,
                    (double)a.Longitude!.Value,
                    latitude,
                    longitude) <= radiusKm)
                .OrderBy(a => CalculateDistance(
                    (double)a.Latitude!.Value,
                    (double)a.Longitude!.Value,
                    latitude,
                    longitude));
        }

        public async Task<IEnumerable<string>> GetLocationsAsync()
        {
            return await _context.Attractions
                .Where(a => !a.IsDeleted && !string.IsNullOrEmpty(a.Location))
                .Select(a => a.Location)
                .Distinct()
                .OrderBy(l => l)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Attractions
                .CountAsync(a => !a.IsDeleted);
        }

        public async Task<int> GetCountByCategoryAsync(AttractionCategory category)
        {
            return await _context.Attractions
                .CountAsync(a => !a.IsDeleted && a.Category == category);
        }

        public async Task<int> GetCountByStatusAsync(AttractionStatus status)
        {
            return await _context.Attractions
                .CountAsync(a => !a.IsDeleted && a.Status == status);
        }

        public async Task<decimal> GetAveragePriceAsync()
        {
            return await _context.Attractions
                .Where(a => !a.IsDeleted)
                .AverageAsync(a => a.Price);
        }

        public async Task<double> GetAverageRatingAsync(int attractionId)
        {
            var attraction = await _context.Attractions
                .FirstOrDefaultAsync(a => a.Id == attractionId && !a.IsDeleted);
            return attraction?.Rating != null ? Convert.ToDouble(attraction.Rating) : 0.0;
        }

        public async Task<int> GetReviewCountAsync(int attractionId)
        {
            var attraction = await _context.Attractions
                .FirstOrDefaultAsync(a => a.Id == attractionId && !a.IsDeleted);
            return attraction?.ReviewCount ?? 0;
        }

        public async Task UpdateRatingAsync(int attractionId, double newRating)
        {
            var attraction = await _context.Attractions
                .FirstOrDefaultAsync(a => a.Id == attractionId && !a.IsDeleted);
                
            if (attraction != null)
            {
                attraction.Rating = (decimal)newRating;
                attraction.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateStatusBatchAsync(IEnumerable<int> ids, AttractionStatus status)
        {
            var attractions = await _context.Attractions
                .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
                .ToListAsync();

            foreach (var attraction in attractions)
            {
                attraction.Status = status;
                attraction.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBatchAsync(IEnumerable<int> ids)
        {
            var attractions = await _context.Attractions
                .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
                .ToListAsync();

            foreach (var attraction in attractions)
            {
                attraction.IsDeleted = true;
                attraction.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Attraction>> GetAttractionsByLocationAsync(double latitude, double longitude, double radiusInKm)
        {
            // This is a simplified implementation. In a real application, you would use a spatial query
            // to find attractions within the specified radius using the Haversine formula
            var attractions = await GetAllAsync();
            return attractions.Where(a => 
                a.Latitude.HasValue && 
                a.Longitude.HasValue && 
                CalculateDistance(latitude, longitude, a.Latitude.Value, a.Longitude.Value) <= radiusInKm);
        }

        public async Task<IEnumerable<Attraction>> GetAttractionsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var attractions = await GetAllAsync();
            return attractions.Where(a => a.Price >= minPrice && a.Price <= maxPrice);
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadiusKm = 6371;
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadiusKm * c;
        }

        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public async Task<IEnumerable<Attraction>> SearchAsync(AttractionsSearchViewModel searchModel)
        {
            var query = _context.Attractions
                .Include(a => a.OperatingHours)
                .Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(searchModel.Keyword))
            {
                query = query.Where(a => a.Name.Contains(searchModel.Keyword, StringComparison.OrdinalIgnoreCase));
            }

            if (searchModel.Category.HasValue)
            {
                query = query.Where(a => a.Category == searchModel.Category.Value);
            }

            if (searchModel.Status.HasValue)
            {
                query = query.Where(a => a.Status == searchModel.Status.Value);
            }

            return await query.OrderBy(a => a.Name).ToListAsync();
        }
    }
} 