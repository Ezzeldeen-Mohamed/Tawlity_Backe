using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class PricingPlan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ?Name { get; set; } // "Basic", "Premium", "Featured"

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int DurationDays { get; set; } // 30 for monthly, 365 for annual

        [StringLength(500)]
        public string ?Features { get; set; } // e.g., "Top placement in search results"
    }
}
