using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class OperatingHours
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DayOfWeek Day { get; set; } // e.g., Monday, Tuesday

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan OpenTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan CloseTime { get; set; }

        // Relationships
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public virtual Branch ?Branch { get; set; }
    }
}
