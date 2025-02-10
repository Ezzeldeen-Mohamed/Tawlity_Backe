using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ?Title { get; set; }

        [StringLength(500)]
        public string ?Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string ?DiscountType { get; set; } // "Percentage", "FixedAmount"

        [Range(0, 100)]
        public decimal DiscountValue { get; set; } // e.g., 20% or $10

        // Relationships
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant ?Restaurant { get; set; }
    }
}
