using Microsoft.EntityFrameworkCore;
using Tawlity_Backend.Data;
using Tawlity_Backend.Dtos;
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
            return await _context.Restaurants.Include(r => r.MenuItems).ToListAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            return await _context.Restaurants
                .Include(r=>r.Tables)
                .Include(r => r.MenuItems)
                .FirstOrDefaultAsync(r => r.Id == id);
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
            var restaurant = await _context.Restaurants
                    .Include(r => r.Reservations)
                    .Include(r => r.Tables)
                    .Include (r => r.MenuItems)
                    .Include(r=>r.User)
                    .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null) return false;

            _context.Restaurants.Remove(restaurant);
            return await _context.SaveChangesAsync() > 0;
        }



        public async Task<IEnumerable<Restaurant>> SearchAsync(string query)
        {
            return await _context.Restaurants
                .Where(r => r.Name.Contains(query) || r.Description.Contains(query))
                .ToListAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetNearbyAsync(double lat, double lon, double radius)
        {
            return await _context.Restaurants
                .Where(r => Math.Sqrt(Math.Pow(r.Latitude - lat, 2) + Math.Pow(r.Longitude - lon, 2)) <= radius)
                .ToListAsync();
        }
    }
}
