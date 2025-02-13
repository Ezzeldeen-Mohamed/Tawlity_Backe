using Microsoft.EntityFrameworkCore;
using Tawlity.Core.Enums;
using Tawlity_Backend.Data;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;

namespace Tawlity_Backend.Repositories.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AnalyticsDto> GetAnalyticsAsync()
        {
            var totalUsers = await _context.Employees.CountAsync();
            var totalRestaurants = await _context.Restaurants.CountAsync();
            var totalReservations = await _context.Reservations.CountAsync();
            var totalRevenue = await _context.Payments.SumAsync(p => p.Amount);

            return new AnalyticsDto
            {
                TotalUsers = totalUsers,
                TotalRestaurants = totalRestaurants,
                TotalReservations = totalReservations,
                TotalRevenue = totalRevenue
            };
        }

        public async Task<bool> SeedRolesAsync()
        {
            // If roles exist, return false
            if (await _context.Employees.AnyAsync(u => u.Employee_Role != null)) return false;

            // Add enum roles (Admin, RestaurantOwner, Customer)
            var admin = new User { EmployeeEmail = "admin@tawlity.com", Employee_Role = Employee_Role.Admin };
            var owner = new User { EmployeeEmail = "owner@tawlity.com", Employee_Role = Employee_Role.RestaurantOwner };
            var customer = new User { EmployeeEmail = "customer@tawlity.com", Employee_Role = Employee_Role.Customer };

            await _context.Employees.AddRangeAsync(admin, owner, customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SeedSampleRestaurantsAsync()
        {
            if (await _context.Restaurants.AnyAsync()) return false;

            var sampleRestaurants = new List<Restaurant>
            {
                new Restaurant { Name = "The Italian Spot", Description = "Authentic Italian cuisine." },
                new Restaurant { Name = "Sushi World", Description = "Fresh sushi and sashimi." }
            };

            await _context.Restaurants.AddRangeAsync(sampleRestaurants);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
