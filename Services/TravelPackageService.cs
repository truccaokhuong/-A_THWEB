using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;

namespace TH_WEB.Services
{
    public class TravelPackageService : ITravelPackageService
    {
        private readonly ApplicationDbContext _context;

        public TravelPackageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TravelPackage>> GetAllAsync()
        {
            return await _context.TravelPackages
                .Include(p => p.Hotel)
                .Include(p => p.CarRental)
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<TravelPackage?> GetByIdAsync(int id)
        {
            return await _context.TravelPackages
                .Include(p => p.Hotel)
                .Include(p => p.CarRental)
                .Include(p => p.Itinerary)
                .Include(p => p.Extras)
                .Include(p => p.FAQs)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public async Task<IEnumerable<TravelPackage>> GetFeaturedPackagesAsync()
        {
            return await _context.TravelPackages
                .Include(p => p.Hotel)
                .Include(p => p.CarRental)
                .Where(p => p.IsActive && p.IsFeatured)
                .OrderBy(p => p.Priority)
                .ToListAsync();
        }

        public async Task<IEnumerable<TravelPackage>> SearchPackagesAsync(
            string? destination = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            decimal? maxPrice = null)
        {
            var query = _context.TravelPackages
                .Include(p => p.Hotel)
                .Include(p => p.CarRental)
                .Where(p => p.IsActive);

            if (!string.IsNullOrWhiteSpace(destination))
            {
                query = query.Where(p => 
                    p.DestinationCode.Contains(destination) ||
                    p.City.Contains(destination) ||
                    p.Country.Contains(destination));
            }

            if (startDate.HasValue)
            {
                query = query.Where(p => p.StartDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(p => p.EndDate <= endDate.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.TotalPrice <= maxPrice.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<decimal> CalculateTotalPriceAsync(int packageId, int adults, int children, int infants)
        {
            var package = await GetByIdAsync(packageId);
            if (package == null)
                throw new ArgumentException("Package not found");

            return (package.AdultPrice * adults) +
                   (package.ChildPrice * children) +
                   (package.InfantPrice * infants);
        }

        public async Task<TravelPackage> CreateAsync(TravelPackage package)
        {
            package.CreatedAt = DateTime.UtcNow;
            package.UpdatedAt = DateTime.UtcNow;
            package.IsActive = true;

            _context.TravelPackages.Add(package);
            await _context.SaveChangesAsync();
            return package;
        }

        public async Task<TravelPackage> UpdateAsync(TravelPackage package)
        {
            package.UpdatedAt = DateTime.UtcNow;
            _context.TravelPackages.Update(package);
            await _context.SaveChangesAsync();
            return package;
        }

        public async Task DeleteAsync(int id)
        {
            var package = await GetByIdAsync(id);
            if (package != null)
            {
                package.IsActive = false;
                package.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePackageStatusAsync(int id, PackageStatus status)
        {
            var package = await GetByIdAsync(id);
            if (package != null)
            {
                package.Status = status;
                package.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
} 