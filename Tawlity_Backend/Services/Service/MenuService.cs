namespace Tawlity_Backend.Services.Service
{
    using AutoMapper;
    using Tawlity_Backend.Models;
    using Tawlity_Backend.Repositories.Interface;
    using Tawlity_Backend.Services.IService;
    using Tawlity_Backend.Dtos;

    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuItemDto>> GetMenuItemsByRestaurantIdAsync(int restaurantId)
        {
            var menuItems = await _menuRepository.GetMenuItemsByRestaurantIdAsync(restaurantId);
            return _mapper.Map<IEnumerable<MenuItemDto>>(menuItems);
        }

        public async Task<MenuItemDto?> GetMenuItemByIdAsync(int id)
        {
            var menuItem = await _menuRepository.GetMenuItemByIdAsync(id);
            return menuItem == null ? null : _mapper.Map<MenuItemDto>(menuItem);
        }

        public async Task AddMenuItemAsync(CreateMenuItemDto menuItemDto)
        {
            var menuItem = _mapper.Map<MenuItem>(menuItemDto);
            await _menuRepository.AddMenuItemAsync(menuItem);
        }
    }

}
