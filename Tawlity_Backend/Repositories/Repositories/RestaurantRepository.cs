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
            => await _context.Restaurants.ToListAsync();

        public async Task<Restaurant?> GetByIdAsync(int id)
            => await _context.Restaurants.FindAsync(id);

        public async Task<Restaurant?> GetByNameAsync(string name)
            => await _context.Restaurants.FirstOrDefaultAsync(r => r.Name == name);

        public async Task<IEnumerable<Restaurant>> SearchAsync(string query)
            => await _context.Restaurants.Where(r => r.Name.Contains(query) || r.Description.Contains(query)|| r.Address.Contains(query)).ToListAsync();

        public async Task<IEnumerable<Restaurant>> GetNearbyAsync(double lat, double lon, double radiusKm)
            => await _context.Restaurants.Where(r =>
                Math.Sqrt(Math.Pow(r.Latitude - lat, 2) + Math.Pow(r.Longitude - lon, 2)) <= radiusKm
            ).ToListAsync();

        public async Task AddAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
