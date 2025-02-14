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

        public async Task<RestaurantDto> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public void CreateRestaurantAsync(CreateRestaurantDto dto)
        {
            if (dto == null)
            {
                throw new NullReferenceException("Enter data");
            }
                _repository.Createrestaurant(dto);
        }


        public async Task UpdateRestaurantAsync(int id, UpdateRestaurantDto dto)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            _mapper.Map(dto, restaurant);
            await _repository.UpdateAsync(restaurant);
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RestaurantDto>> SearchRestaurantsAsync(string query)
        {
            var restaurants = await _repository.SearchAsync(query);
            return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        }

        public async Task<IEnumerable<RestaurantDto>> GetNearbyRestaurantsAsync(double latitude, double longitude, double radiusKm)
        {
            var restaurants = await _repository.GetNearbyAsync(latitude, longitude, radiusKm);
            return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        }
    }
}
