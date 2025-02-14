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
            return await _context.Restaurants
                .Include(r => r.Branches)
                .Include(r => r.MenuItems)
                .ToListAsync();
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            return await _context.Restaurants
                .Include(r => r.Branches)
                .Include(r => r.MenuItems)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Restaurant> CreateAsync(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }
        public void Createrestaurant(CreateRestaurantDto gen)
        {
            var rest = new Restaurant
            {
                MenuItems =gen .MenuItems.Select(x => new MenuItem
                {
                    Description = x.Description,
                    Name = x.Name,
                    Price = x.Price
                }).ToList(),
                Name = gen.Name,
                Description = gen.Description,
                Address = gen.Address,
                Latitude = gen.Latitude,
                Longitude = gen.Longitude,
                Phone = gen.Phone,
            };
            _context.Restaurants.Add(rest);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            _context.Entry(restaurant).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Restaurant>> SearchAsync(string query)
        {
            return await _context.Restaurants
                .Where(r => r.Name.Contains(query) || r.Description.Contains(query))
                .ToListAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetNearbyAsync(double latitude, double longitude, double radiusKm)
        {
            const double kmToDegrees = 1 / 111.0; // Approximate conversion
            var delta = radiusKm * kmToDegrees;

            return await _context.Restaurants
                .Include(r => r.Branches)
                .Where(r => r.Branches.Any(b =>
                    b.Latitude >= latitude - delta &&
                    b.Latitude <= latitude + delta &&
                    b.Longitude >= longitude - delta &&
                    b.Longitude <= longitude + delta))
                .ToListAsync();
        }

      
    }
}
