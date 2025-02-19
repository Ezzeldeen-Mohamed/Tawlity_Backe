using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Dtos
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? MenuItemImage { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
    }   
    public class MenuItemD
    {
        public string Name { get; set; }
        public string? MenuItemImage { get; set; }

        public decimal Price { get; set; }
    } 
    public class CreateMetemDto
    {
        [Required]
        public int RestaurantId { get; set; }
        public string? MenuItemImage { get; set; } 

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }
    }
    public class OrderItemDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}
