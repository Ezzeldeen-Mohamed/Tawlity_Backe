using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Dtos
{
    public class CreateTableDto
    {
        [Required]
        [Range(1, 20)]
        public int Capacity { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int RestaurantId { get; set; } // The branch where the table belongs
    }
    public class TableDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string? ImageUrl { get; set; }
        public int RestaurantId { get; set; }
    }
   
}
