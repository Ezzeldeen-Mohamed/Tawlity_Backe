using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Services.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<PaymentResponseDto>> GetPaymentsByUserIdAsync(int userId)
        {
            var payments = await _paymentRepository.GetPaymentsByUserIdAsync(userId);
            return payments.Select(p => new PaymentResponseDto
            {
                PaymentId = p.Id,
                UserId = p.UserId,
                Amount = p.Amount,
                Status = p.Status,
                TransactionId = p.TransactionId,
                PaymentDate = p.PaymentDate
            }).ToList();
        }

        public async Task<PaymentResponseDto?> ProcessPaymentAsync(PaymentRequestDto paymentDto)
        {
            // Fake Transaction ID (in real case, integrate Stripe/PayPal API)
            string transactionId = Guid.NewGuid().ToString();

            var payment = new Payment
            {
                UserId = paymentDto.UserId,
                Amount = paymentDto.Amount,
                PaymentMethod = paymentDto.PaymentMethod,
                TransactionId = transactionId,
                Status = "Completed", // Assume success
                PaymentDate = DateTime.UtcNow
            };

            await _paymentRepository.AddPaymentAsync(payment);

            return new PaymentResponseDto
            {
                PaymentId = payment.Id,
                UserId = payment.UserId,
                Amount = payment.Amount,
                Status = payment.Status,
                TransactionId = payment.TransactionId,
                PaymentDate = payment.PaymentDate
            };
        }
    }
}
