using Tawlity_Backend.Dtos;

namespace Tawlity_Backend.Services.IService
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentResponseDto>> GetPaymentsByUserIdAsync(int userId);
        Task<PaymentResponseDto?> ProcessPaymentAsync(PaymentRequestDto paymentDto);
    }
}
