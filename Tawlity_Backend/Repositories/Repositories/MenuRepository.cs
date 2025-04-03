using Microsoft.EntityFrameworkCore;
using Tawlity_Backend.Data;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;

public class MenuRepository : IMenuRepository
{
    private readonly AppDbContext _context;

    public MenuRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MenuItem>> GetMenuItemsByRestaurantIdAsync(int restaurantId)
    {
        return await _context.MenuItems
            .Where(m => m.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task<MenuItem?> GetMenuItemByIdAsync(int id)
    {
        return await _context.MenuItems.FindAsync(id);
    }
    public async Task<MenuItem?> GetMenuItemByNameAsync(string name)
    {
        return await _context.MenuItems
            .AsNoTracking()  // 🚀 يحسن الأداء ويمنع المشاكل عند الحفظ
            .FirstOrDefaultAsync(m => m.Name == name);
    }

    public async Task AddMenuItemAsync(MenuItem menuItem)
    {
        await _context.MenuItems.AddAsync(menuItem);
        await _context.SaveChangesAsync();
    }
}
