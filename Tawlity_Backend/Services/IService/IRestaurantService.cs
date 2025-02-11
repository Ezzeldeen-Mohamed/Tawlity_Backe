using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Services.IService
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task<Restaurant> GetRestaurantByIdAsync(int id);
        Task<IEnumerable<Restaurant>> SearchRestaurantsAsync(string query);
        Task<IEnumerable<Restaurant>> GetNearbyRestaurantsAsync(double lat, double lon, double radius);
        Task<Restaurant> CreateRestaurantAsync(RestaurantDto dto);
        Task<bool> UpdateRestaurantAsync(int id, RestaurantDto dto);
        Task<bool> DeleteRestaurantAsync(int id);
    }
}
