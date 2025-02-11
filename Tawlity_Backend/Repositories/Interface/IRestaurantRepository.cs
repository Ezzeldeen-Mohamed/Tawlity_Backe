using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant> GetByIdAsync(int id);
        Task<IEnumerable<Restaurant>> SearchByNameAsync(string query);
        Task<IEnumerable<Restaurant>> GetNearbyRestaurantsAsync(double latitude, double longitude, double radius);
        Task<Restaurant> CreateAsync(Restaurant restaurant);
        Task<bool> UpdateAsync(Restaurant restaurant);
        Task<bool> DeleteAsync(int id);
    }
}

