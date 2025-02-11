using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Services.IService
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
        Task<RestaurantDto?> GetRestaurantByIdAsync(int id);
        Task<RestaurantDto?> GetRestaurantByNameAsync(string name);
        Task<IEnumerable<RestaurantDto>> SearchRestaurantsAsync(string query);
        Task<IEnumerable<RestaurantDto>> GetNearbyRestaurantsAsync(double lat, double lon, double radiusKm);
        Task AddRestaurantAsync(RestaurantDto dto);
        Task UpdateRestaurantAsync(int id, RestaurantDto dto);
        Task DeleteRestaurantAsync(int id);
    }
}
