using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant> GetByIdAsync(int id);
        Task<Restaurant> CreateAsync(Restaurant restaurant);
        void Createrestaurant(CreateRestaurantDto createGenreDto);
        Task UpdateAsync(Restaurant restaurant);
        Task DeleteAsync(int id);
        Task<IEnumerable<Restaurant>> SearchAsync(string query);
        Task<IEnumerable<Restaurant>> GetNearbyAsync(double latitude, double longitude, double radiusKm);
    }
}

