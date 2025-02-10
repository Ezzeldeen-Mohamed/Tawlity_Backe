using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class DietaryTag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ?Name { get; set; } // "Vegan", "Halal", "Gluten-Free"

        // Relationships
        public virtual ICollection<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();
    }
}
