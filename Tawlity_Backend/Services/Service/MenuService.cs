namespace Tawlity_Backend.Services.Service
{
    using AutoMapper;
    using Tawlity_Backend.Models;
    using Tawlity_Backend.Repositories.Interface;
    using Tawlity_Backend.Services.IService;
    using Tawlity_Backend.Dtos;
    using Tawlity_Backend.Repositories.Repositories;

    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<IEnumerable<MenuItemDto>> GetMenuItemsByRestaurantIdAsync(int restaurantId)
        {
            var menuItems = await _menuRepository.GetMenuItemsByRestaurantIdAsync(restaurantId);
            return menuItems.Select(m => new MenuItemDto
            {
                Id = m.Id,
                Name = m.Name,
                MenuItemImage= m.MenuItemImage,
                Description = m.Description,
                Price = m.Price,
                RestaurantId = m.RestaurantId
            }).ToList();
        }

        public async Task<MenuItemDto?> GetMenuItemByIdAsync(int id)
        {
            var menuItem = await _menuRepository.GetMenuItemByIdAsync(id);
            if (menuItem == null) return null;

            return new MenuItemDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                MenuItemImage = menuItem.MenuItemImage,
                Description = menuItem.Description,
                Price = menuItem.Price,
                RestaurantId = menuItem.RestaurantId
            };
        }
        public async Task<MenuItemD?> GetMenuItemByNameAsync(MenuItemD menuItemD)
        {
            var menuItem = await _menuRepository.GetMenuItemByNameAsync(menuItemD.Name);
            if (menuItem == null) return null;

            return new MenuItemD
            {
                Name = menuItem.Name,
                //Price=menuItem.Price
            };
        }

        public async Task AddMenuItemAsync(CreateMetemDto menuItemDto)
        {
            // Check if the restaurant exists before adding a menu item
            var restaurantExists = await _menuRepository.GetMenuItemsByRestaurantIdAsync(menuItemDto.RestaurantId);
            if (restaurantExists == null)
                throw new Exception($"Restaurant with ID {menuItemDto.RestaurantId} not found!");

            var menuItem = new MenuItem
            {
                RestaurantId=menuItemDto.RestaurantId,
                MenuItemImage=menuItemDto.MenuItemImage,
                Name = menuItemDto.Name,
                Description = menuItemDto.Description,
                Price = menuItemDto.Price
            };

            await _menuRepository.AddMenuItemAsync(menuItem);
        }
    }

}
