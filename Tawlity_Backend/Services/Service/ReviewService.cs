using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Services.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByRestaurantIdAsync(int restaurantId)
        {
            var reviews = await _reviewRepository.GetReviewsByRestaurantIdAsync(restaurantId);
            return reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                UserId = r.UserId,
                RestaurantId = r.RestaurantId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToList();
        }

        public async Task<bool> AddReviewAsync(CreateReviewDto reviewDto)
        {
            var review = new Review
            {
                UserId = reviewDto.UserId,
                RestaurantId = reviewDto.RestaurantId,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _reviewRepository.AddReviewAsync(review);
            return true;
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(reviewId);
            if (review == null) return false;

            await _reviewRepository.DeleteReviewAsync(reviewId);
            return true;
        }
    }

}
