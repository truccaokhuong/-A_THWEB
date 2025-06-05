using TH_WEB.Models;

namespace TH_WEB.Services
{
    public interface ICarRentalService
    {
        Task<IEnumerable<CarRental>> GetAllAsync();
        Task<CarRental?> GetByIdAsync(int id);
        Task<CarRental> CreateAsync(CarRental carRental);
        Task<CarRental> UpdateAsync(CarRental carRental);
        Task DeleteAsync(int id);
        Task<IEnumerable<CarRental>> SearchCarRentalsAsync(int pickupLocationId, int dropoffLocationId, DateTime pickupDate, DateTime dropoffDate, CarCategory? category = null);
        Task<IEnumerable<CarRental>> GetAvailableCarRentalsAsync();
    }
} 