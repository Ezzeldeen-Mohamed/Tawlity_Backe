using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ?Name { get; set; } // "Admin", "RestaurantOwner", "Customer"
        public virtual  User? User { get; set; }
    }
}
