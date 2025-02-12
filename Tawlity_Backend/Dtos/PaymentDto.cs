using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Dtos
{
    public class PaymentRequestDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string PaymentMethod { get; set; } // "Stripe" or "PayPal"
    }

    public class PaymentResponseDto
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string TransactionId { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
