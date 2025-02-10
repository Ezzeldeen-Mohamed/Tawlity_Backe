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
        public string ?TransactionId { get; set; } // PayPal/Stripe ID

        [Required]
        public DateTime PaymentDate { get; set; }

        // Relationships
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User ?User { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant ?Restaurant { get; set; }
    }
}
