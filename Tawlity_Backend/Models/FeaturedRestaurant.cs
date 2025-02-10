using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class FeaturedRestaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public decimal PaymentAmount { get; set; }

        [Required]
        public string ?Status { get; set; } // "Active", "Expired"

        // Relationships
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant ?Restaurant { get; set; }

        [ForeignKey("PricingPlan")]
        public int PlanId { get; set; }
        public virtual PricingPlan? PricingPlan { get; set; }
    }
}
