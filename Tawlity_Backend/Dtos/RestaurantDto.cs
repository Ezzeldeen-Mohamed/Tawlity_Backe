using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Dtos
{
    public class CreateRestaurantDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public string? Address { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public int UserId { get; set; }  // مالك المطعم

        public List<CreateMenuItemDto> MenuItems { get; set; } = new();
    }
    public class UpdateRestaurantDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public string? Address { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }


    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int UserId { get; set; }
    }
    public class CreateMenuItemDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }
    }

}

