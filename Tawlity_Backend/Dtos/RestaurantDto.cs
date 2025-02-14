using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Dtos
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public IEnumerable<BranchDto> Branches { get; set; }
        public IEnumerable<MenuItemDto> MenuItems { get; set; }
    }

    public class CreateRestaurantDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Phone]
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
        public IEnumerable<CreateMenuItemDto> MenuItems { get; set; }

    }

    public class UpdateRestaurantDto
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Phone]
        public string Phone { get; set; }
    }


}
