using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Services.IService
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllAsync();
        Task<RestaurantDto?> GetByIdAsync(int id);
        Task<RestaurantDto> CreateAsync(CreateRestaurantDto dto);
        Task<bool> UpdateAsync(int id, UpdateRestaurantDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<RestaurantDto>> SearchAsync(string query);
        Task<IEnumerable<RestaurantDto>> GetNearbyAsync(double lat, double lon, double radius);
    }

}
