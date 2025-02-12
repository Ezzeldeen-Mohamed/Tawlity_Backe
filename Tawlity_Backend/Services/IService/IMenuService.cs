using Tawlity_Backend.Dtos;

namespace Tawlity_Backend.Services.IService
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuItemDto>> GetMenuItemsByRestaurantIdAsync(int restaurantId);
        Task<MenuItemDto?> GetMenuItemByIdAsync(int id);
        Task AddMenuItemAsync(CreateMenuItemDto menuItemDto);
    }
}
