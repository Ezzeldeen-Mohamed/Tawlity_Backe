using System.ComponentModel.DataAnnotations;

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

            [Phone]
            public string ?Phone { get; set; }
            public string ?Address { get; set; }
            [Required]
            public double Latitude { get; set; } 

            [Required]
            public double Longitude { get; set; }

        // Relationships
        public virtual ICollection<Image> Images { get; set; }

            public virtual ICollection<Branch> Branches { get; set; } = new HashSet<Branch>();
            public virtual ICollection<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();
            public virtual ICollection<Promotion> Promotions { get; set; } = new HashSet<Promotion>();
            public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
            public virtual ICollection<FeaturedRestaurant> FeaturedPlans { get; set; } = new HashSet<FeaturedRestaurant>();
        
        }
}
