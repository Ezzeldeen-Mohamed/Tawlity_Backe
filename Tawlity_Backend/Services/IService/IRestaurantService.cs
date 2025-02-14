using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Services.IService
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
        Task<RestaurantDto> GetRestaurantByIdAsync(int id);
        void CreateRestaurantAsync(CreateRestaurantDto dto);

        Task UpdateRestaurantAsync(int id, UpdateRestaurantDto dto);
        Task DeleteRestaurantAsync(int id);
        Task<IEnumerable<RestaurantDto>> SearchRestaurantsAsync(string query);
        Task<IEnumerable<RestaurantDto>> GetNearbyRestaurantsAsync(double latitude, double longitude, double radiusKm);
    }
}
