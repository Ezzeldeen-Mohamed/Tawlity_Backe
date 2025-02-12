using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tawlity_Backend.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal Amount { get; set; }

        [Required]
        public string PaymentMethod { get; set; } // "Stripe" or "PayPal"

        [Required]
        public string TransactionId { get; set; } // From Stripe/PayPal

        [Required]
        public string Status { get; set; } // "Completed", "Failed", etc.

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        // Relationships
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User ?User { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant ?Restaurant { get; set; }
    }
}
