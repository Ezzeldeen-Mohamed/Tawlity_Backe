using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<Restaurant?> GetByNameAsync(string name);
        Task<IEnumerable<Restaurant>> SearchAsync(string query);
        Task<IEnumerable<Restaurant>> GetNearbyAsync(double lat, double lon, double radiusKm);
        Task AddAsync(Restaurant restaurant);
        Task UpdateAsync(Restaurant restaurant);
        Task DeleteAsync(Restaurant restaurant);
        Task SaveChangesAsync();
    }
}

