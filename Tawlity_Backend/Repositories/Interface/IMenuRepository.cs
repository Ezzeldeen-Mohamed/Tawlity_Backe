using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface IMenuRepository
    {
        Task<IEnumerable<MenuItem>> GetMenuItemsByRestaurantIdAsync(int restaurantId);
        Task<MenuItem?> GetMenuItemByIdAsync(int id);
        Task AddMenuItemAsync(MenuItem menuItem);
    }
}
