using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TH_WEB.Areas.Attractions.Models;
using TH_WEB.Areas.Attractions.ViewModels;

namespace TH_WEB.Areas.Attractions.Repositories
{
    public interface IAttractionsRepository
    {
        // Basic CRUD operations
        Task<IEnumerable<Attraction>> GetAllAsync();
        Task<Attraction> GetByIdAsync(int id);
        Task<Attraction> CreateAsync(Attraction attraction);
        Task<Attraction> UpdateAsync(Attraction attraction);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        // Search and filtering
        Task<IEnumerable<Attraction>> SearchAsync(string? name = null, AttractionCategory? category = null, AttractionStatus? status = null);
        Task<IEnumerable<Attraction>> GetByLocationAsync(string location);
        Task<IEnumerable<Attraction>> GetByCategoryAsync(AttractionCategory category);
        Task<IEnumerable<Attraction>> GetByStatusAsync(AttractionStatus status);
        Task<IEnumerable<Attraction>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);

        // Popular and featured attractions
        Task<IEnumerable<Attraction>> GetPopularAsync(int count = 10);
        Task<IEnumerable<Attraction>> GetFeaturedAsync(int count = 5);
        Task<IEnumerable<Attraction>> GetRecentAsync(int count = 10);

        // Location-based queries
        Task<IEnumerable<Attraction>> GetNearbyAsync(double latitude, double longitude, double radiusKm);
        Task<IEnumerable<string>> GetLocationsAsync();

        // Statistics
        Task<int> GetTotalCountAsync();
        Task<int> GetCountByCategoryAsync(AttractionCategory category);
        Task<int> GetCountByStatusAsync(AttractionStatus status);
        Task<decimal> GetAveragePriceAsync();

        // Reviews and ratings
        Task<double> GetAverageRatingAsync(int attractionId);
        Task<int> GetReviewCountAsync(int attractionId);
        Task UpdateRatingAsync(int attractionId, double newRating);

        // Batch operations
        Task<bool> UpdateStatusBatchAsync(IEnumerable<int> ids, AttractionStatus status);
        Task<bool> DeleteBatchAsync(IEnumerable<int> ids);

        // New methods
        Task<IEnumerable<Attraction>> SearchAsync(AttractionsSearchViewModel searchModel);
        Task<IEnumerable<Attraction>> GetAttractionsByLocationAsync(double latitude, double longitude, double radiusInKm);
        Task<IEnumerable<Attraction>> GetAttractionsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
} 