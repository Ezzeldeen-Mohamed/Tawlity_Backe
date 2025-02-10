using Microsoft.EntityFrameworkCore;
using Tawlity_Backend.Data;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;

namespace Tawlity_Backend.Repositories.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly AppDbContext _context;

        public RestaurantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            return await _context.Restaurants.FindAsync(id);
        }

        public async Task<IEnumerable<Restaurant>> SearchByNameAsync(string query)
        {
            return await _context.Restaurants
                .Where(r => r.Name.Contains(query))
                .ToListAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetNearbyRestaurantsAsync(double latitude, double longitude, double radius)
        {
            return await _context.Restaurants
                .Where(r => EF.Functions.Like(r.Address, $"%{latitude},{longitude}%")) // Placeholder, you need actual geolocation logic
                .ToListAsync();
        }

        public async Task<Restaurant> CreateAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }

        public async Task<bool> UpdateAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var restaurant = await GetByIdAsync(id);
            if (restaurant == null) return false;

            _context.Restaurants.Remove(restaurant);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
