using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TH_WEB.Models;

namespace TH_WEB.Services
{
    public interface ITravelPackageService
    {
        Task<IEnumerable<TravelPackage>> GetAllAsync();
        Task<TravelPackage?> GetByIdAsync(int id);
        Task<IEnumerable<TravelPackage>> GetFeaturedPackagesAsync();
        Task<IEnumerable<TravelPackage>> SearchPackagesAsync(string destination, DateTime? startDate = null, DateTime? endDate = null, decimal? maxPrice = null);
        Task<decimal> CalculateTotalPriceAsync(int packageId, int adults, int children, int infants);
        Task<TravelPackage> CreateAsync(TravelPackage package);
        Task<TravelPackage> UpdateAsync(TravelPackage package);
        Task DeleteAsync(int id);
        Task UpdatePackageStatusAsync(int id, PackageStatus status);
    }
} 