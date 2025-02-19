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

        public RestaurantService(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
        {
            var restaurants = await _repository.GetAllAsync();
            return restaurants.Select(r => new RestaurantDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                RestaurantImage=r.RestaurantImage,
                Phone = r.Phone,
                Address = r.Address,
                Latitude = r.Latitude,
                Longitude = r.Longitude,
                UserId = r.UserId,
                MenuItems = r.MenuItems.Select(x => new CreateMenuItemDto
                {
                    Name = x.Name,
                    MenuItemImage=x.MenuItemImage,
                    Price = x.Price,
                    Description = x.Description
                }).ToList(),
            });
        }

        public async Task<RestaurantDto?> GetByIdAsync(int id)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            if (restaurant == null) return null;

            return new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                RestaurantImage=restaurant.RestaurantImage,
                Description = restaurant.Description,
                Phone = restaurant.Phone,
                Address = restaurant.Address,
                Latitude = restaurant.Latitude,
                Longitude = restaurant.Longitude,
                UserId = restaurant.UserId,
                MenuItems = restaurant.MenuItems.Select(x => new CreateMenuItemDto
                {
                    Name = x.Name,
                    MenuItemImage=x.MenuItemImage,
                    Price = x.Price,
                    Description = x.Description
                }).ToList(),
            };
        }
        public async Task<CreateRestaurantwithmenuDto?> GetByIdWithMenuAsync(int id)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            if (restaurant == null) return null;

            return new CreateRestaurantwithmenuDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                RestaurantImage=restaurant.RestaurantImage,
                Phone = restaurant.Phone,
                Address = restaurant.Address,
                Latitude = restaurant.Latitude,
                Longitude = restaurant.Longitude,
                UserId = restaurant.UserId,
                MenuItems=restaurant.MenuItems.Select(x=>new CreateMenuItemDto
                {
                    Name=x.Name,
                    MenuItemImage=x.MenuItemImage,
                    Price=x.Price,
                    Description=x.Description
                } ).ToList(),
            };
        }

        public async Task<RestaurantDto> CreateAsync(CreateRestaurantDto dto)
        {
            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Description = dto.Description,
                RestaurantImage = dto.RestaurantImage,
                Phone = dto.Phone,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                UserId = dto.UserId
            };

            restaurant.MenuItems = dto.MenuItems.Select(m => new MenuItem
            {
                Name = m.Name,
                MenuItemImage = m.MenuItemImage,
                Description = m.Description,
                Price = m.Price
            }).ToList();

            await _repository.CreateAsync(restaurant);


            return new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                RestaurantImage=restaurant.RestaurantImage,
                Description = restaurant.Description,
                Phone = restaurant.Phone,
                Address = restaurant.Address,
                Latitude = restaurant.Latitude,
                Longitude = restaurant.Longitude,
                UserId = restaurant.UserId
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateRestaurantDto dto)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            if (restaurant == null) return false;

            restaurant.Name = dto.Name;
            restaurant.RestaurantImage = dto.RestaurantImage;
            restaurant.Description = dto.Description;
            restaurant.Phone = dto.Phone;
            restaurant.Address = dto.Address;
            restaurant.Latitude = dto.Latitude;
            restaurant.Longitude = dto.Longitude;

            return await _repository.UpdateAsync(restaurant);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RestaurantDto>> SearchAsync(string query)
        {
            var restaurants = await _repository.SearchAsync(query);
            return restaurants.Select(r => new RestaurantDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Phone = r.Phone,
                Address = r.Address,
                Latitude = r.Latitude,
                RestaurantImage=r.RestaurantImage,
                Longitude = r.Longitude,
                UserId = r.UserId
            });
        }


        public async Task<IEnumerable<RestaurantDto>> GetNearbyAsync(double lat, double lon, double radius)
        {
            var restaurants = await _repository.GetNearbyAsync(lat, lon, radius);
            return restaurants.Select(r => new RestaurantDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                RestaurantImage= r.RestaurantImage,
                Phone = r.Phone,
                Address = r.Address,
                Latitude = r.Latitude,
                Longitude = r.Longitude,
                UserId = r.UserId
            });
        }
    }

}
