using Tawlity_Backend.Dtos;

namespace Tawlity_Backend.Services.IService
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetReviewsByRestaurantIdAsync(int restaurantId);
        Task<bool> AddReviewAsync(CreateReviewDto reviewDto);
        Task<bool> DeleteReviewAsync(int reviewId);
    } 
}
