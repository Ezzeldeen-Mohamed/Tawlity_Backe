using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId);
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        Task AddPaymentAsync(Payment payment);
    }
}
