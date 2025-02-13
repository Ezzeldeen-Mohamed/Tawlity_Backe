using Tawlity_Backend.Dtos;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface IAdminRepository
    {
        Task<AnalyticsDto> GetAnalyticsAsync();
        Task<bool> SeedRolesAsync();
        Task<bool> SeedSampleRestaurantsAsync();
    }
}
