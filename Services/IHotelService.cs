// Services/IHotelService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using TH_WEB.Models;

namespace TH_WEB.Services
{
    public interface IHotelService
    {
        Task<IEnumerable<Hotel>> GetAllHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int id);
        Task<IEnumerable<Hotel>> GetFeaturedHotelsAsync();
        Task<IEnumerable<Hotel>> SearchHotelsAsync(string searchTerm);
    }
}