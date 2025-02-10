using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ?Url { get; set; }

        public string ?Description { get; set; }

        // Relationships  
        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }
        public virtual Restaurant ?Restaurant { get; set; }

        [ForeignKey("Table")]
        public int? TableId { get; set; }
        public virtual Table ?Table { get; set; }
    }
}
