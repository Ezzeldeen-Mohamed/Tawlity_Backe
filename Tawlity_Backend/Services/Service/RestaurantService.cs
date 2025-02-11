using AutoMapper;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Services.Service
{

    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repository;
        private readonly IMapper _mapper;

        public RestaurantService(IRestaurantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
        {
            var restaurants = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        }

        public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<RestaurantDto?> GetRestaurantByNameAsync(string name)
        {
            var restaurant = await _repository.GetByNameAsync(name);
            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<IEnumerable<RestaurantDto>> SearchRestaurantsAsync(string query)
        {
            var restaurants = await _repository.SearchAsync(query);
            return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        }

        public async Task<IEnumerable<RestaurantDto>> GetNearbyRestaurantsAsync(double lat, double lon, double radiusKm)
        {
            var restaurants = await _repository.GetNearbyAsync(lat, lon, radiusKm);
            return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        }

        public async Task AddRestaurantAsync(RestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            await _repository.AddAsync(restaurant);
        }

        public async Task UpdateRestaurantAsync(int id, RestaurantDto dto)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            if (restaurant == null) throw new KeyNotFoundException("Restaurant not found");

            _mapper.Map(dto, restaurant);
            await _repository.UpdateAsync(restaurant);
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            if (restaurant == null) throw new KeyNotFoundException("Restaurant not found");

            await _repository.DeleteAsync(restaurant);
        }
    }
}
