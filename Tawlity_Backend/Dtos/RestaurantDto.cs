using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Dtos
{
    public class RestaurantDto
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        public string Address { get; set; }
    }

}
