using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;

namespace TH_WEB.Services
{
    public class CarRentalService : ICarRentalService
    {
        private readonly ApplicationDbContext _context;

        public CarRentalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarRental>> GetAllAsync()
        {
            return await _context.CarRentals
                .Include(cr => cr.CarType)
                .Include(cr => cr.Location)
                .Include(cr => cr.PickupLocation)
                .Include(cr => cr.DropoffLocation)
                .Where(cr => cr.IsActive)
                .ToListAsync();
        }

        public async Task<CarRental?> GetByIdAsync(int id)
        {
            return await _context.CarRentals
                .Include(cr => cr.CarType)
                .Include(cr => cr.Location)
                .Include(cr => cr.PickupLocation)
                .Include(cr => cr.DropoffLocation)
                .Include(cr => cr.CarRentalBookings)
                .Include(cr => cr.Extras)
                .FirstOrDefaultAsync(cr => cr.Id == id && cr.IsActive);
        }

        public async Task<IEnumerable<CarRental>> SearchCarRentalsAsync(
            int locationId,
            int pickupLocationId,
            DateTime startDate,
            DateTime endDate,
            CarCategory? category = null)
        {
            var query = _context.CarRentals
                .Include(cr => cr.CarType)
                .Where(cr => cr.IsActive && cr.LocationId == locationId && cr.PickupLocationId == pickupLocationId);

            if (category.HasValue)
            {
                query = query.Where(cr => cr.CarType.Category == category.Value);
            }

            // Filter out cars that are booked during the requested period
            query = query.Where(cr => !cr.CarRentalBookings.Any(
                b => b.Status != BookingStatus.Cancelled &&
                     ((startDate >= b.StartDate && startDate < b.EndDate) ||
                      (endDate > b.StartDate && endDate <= b.EndDate) ||
                      (startDate <= b.StartDate && endDate >= b.EndDate))
            ));

            return await query.ToListAsync();
        }

        public async Task<decimal> CalculatePriceAsync(int carRentalId, DateTime startDate, DateTime endDate)
        {
            var carRental = await GetByIdAsync(carRentalId);
            if (carRental == null)
                throw new ArgumentException("Car Rental not found");

            var rentalDays = (int)(endDate - startDate).TotalDays + 1;
            return carRental.DailyRate * rentalDays;
        }

        public async Task<CarRental> CreateAsync(CarRental carRental)
        {
            carRental.CreatedAt = DateTime.UtcNow;
            carRental.UpdatedAt = DateTime.UtcNow;
            carRental.IsActive = true;

            _context.CarRentals.Add(carRental);
            await _context.SaveChangesAsync();
            return carRental;
        }

        public async Task<CarRental> UpdateAsync(CarRental carRental)
        {
            carRental.UpdatedAt = DateTime.UtcNow;
            _context.CarRentals.Update(carRental);
            await _context.SaveChangesAsync();
            return carRental;
        }

        public async Task DeleteAsync(int id)
        {
            var carRental = await GetByIdAsync(id);
            if (carRental != null)
            {
                carRental.IsActive = false;
                carRental.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCarRentalStatusAsync(int id, bool isAvailable)
        {
            var carRental = await GetByIdAsync(id);
            if (carRental != null)
            {
                carRental.IsAvailable = isAvailable;
                carRental.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CarRental>> GetAllCarRentalsAsync()
        {
            return await _context.CarRentals
                .Include(c => c.CarType)
                .Include(c => c.Location)
                .OrderBy(c => c.DailyRate)
                .ToListAsync();
        }

        public async Task<CarRental> GetCarRentalByIdAsync(int id)
        {
            return await _context.CarRentals
                .Include(c => c.CarType)
                .Include(c => c.Location)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CarRental>> GetAvailableCarRentalsAsync()
        {
            return await _context.CarRentals
                .Include(c => c.PickupLocation)
                .Include(c => c.DropoffLocation)
                .Where(c => c.IsActive && c.IsAvailable)
                .ToListAsync();
        }
    }
} 