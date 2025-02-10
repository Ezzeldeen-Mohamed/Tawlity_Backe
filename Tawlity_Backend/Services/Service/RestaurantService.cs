using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Services.Service
{

    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repository;

        public RestaurantService(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Restaurant>> SearchRestaurantsAsync(string query)
        {
            return await _repository.SearchByNameAsync(query);
        }

        public async Task<IEnumerable<Restaurant>> GetNearbyRestaurantsAsync(double lat, double lon, double radius)
        {
            return await _repository.GetNearbyRestaurantsAsync(lat, lon, radius);
        }

        public async Task<Restaurant> CreateRestaurantAsync(RestaurantDto dto)
        {
            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Description = dto.Description,
                Phone = dto.Phone,
                Address = dto.Address
            };

            return await _repository.CreateAsync(restaurant);
        }

        public async Task<bool> UpdateRestaurantAsync(int id, RestaurantDto dto)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            if (restaurant == null) return false;

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.Phone = dto.Phone;
            restaurant.Address = dto.Address;

            return await _repository.UpdateAsync(restaurant);
        }

        public async Task<bool> DeleteRestaurantAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
