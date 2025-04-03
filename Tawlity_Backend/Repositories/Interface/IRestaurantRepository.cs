using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<Restaurant> CreateAsync(Restaurant restaurant);
        Task<bool> UpdateAsync(Restaurant restaurant);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Restaurant>> SearchAsync(string query);
        Task<IEnumerable<Restaurant>> GetNearbyAsync(double lat, double lon, double radius);
    }
}

