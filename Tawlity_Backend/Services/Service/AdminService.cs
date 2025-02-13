using Tawlity_Backend.Dtos;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Services.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<AnalyticsDto> GetAnalyticsAsync()
        {
            return await _adminRepository.GetAnalyticsAsync();
        }

        public async Task<bool> SeedRolesAsync()
        {
            return await _adminRepository.SeedRolesAsync();
        }

        public async Task<bool> SeedSampleRestaurantsAsync()
        {
            return await _adminRepository.SeedSampleRestaurantsAsync();
        }
    }
}
