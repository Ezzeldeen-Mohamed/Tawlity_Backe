using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsByRestaurantIdAsync(int restaurantId);
        Task<Review?> GetReviewByIdAsync(int reviewId);
        Task AddReviewAsync(Review review);
        Task DeleteReviewAsync(int reviewId);
    }
}
