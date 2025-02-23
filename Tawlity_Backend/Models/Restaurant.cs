using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tawlity.Core.Enums;

namespace Tawlity_Backend.Models
{
        public class Restaurant
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [StringLength(100)]
            public string ?Name { get; set; }

            [StringLength(500)]
            public string ?Description { get; set; }
            public string ? RestaurantImage {  get; set; }
            [Phone]
            public string ?Phone { get; set; }
            public string ?Address { get; set; }
            [Required]
            public double Latitude { get; set; } 

            [Required]
            public double Longitude { get; set; }

        // Relationships


            [ForeignKey("User")]
            public int UserId { get; set; }
            public virtual User? User { get; set; }
            public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();  
            public virtual ICollection<Table> Tables { get; set; } = new HashSet<Table>();  
            public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();  
        
            public virtual ICollection<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();
        
        }
}
