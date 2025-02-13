using Tawlity_Backend.Dtos;

namespace Tawlity_Backend.Services.IService
{
    public interface IAdminService
    {
        Task<AnalyticsDto> GetAnalyticsAsync();
        Task<bool> SeedRolesAsync();
        Task<bool> SeedSampleRestaurantsAsync();
    }
}
