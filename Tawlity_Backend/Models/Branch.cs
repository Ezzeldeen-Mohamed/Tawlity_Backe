using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public double Latitude { get; set; } // For Google Maps

        [Required]
        public double Longitude { get; set; }

        // Relationships
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; } = new Restaurant();

        public virtual ICollection<Table> Tables { get; set; }=new HashSet<Table>();
        public virtual ICollection<Reservation> Reservations { get; set; }=new HashSet<Reservation>();
        public virtual ICollection<OperatingHours> OperatingHours { get; set; } = new HashSet<OperatingHours>();
    }
}
