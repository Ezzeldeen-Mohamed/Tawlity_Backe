using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tawlity_Backend.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ?Name { get; set; }

        [StringLength(500)]
        public string ?Description { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Price { get; set; }

        // Relationships
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant ?Restaurant { get; set; }

        public virtual ICollection<OrderItem>  OrderItems { get; set; }= new HashSet<OrderItem>();
        
    }
}
