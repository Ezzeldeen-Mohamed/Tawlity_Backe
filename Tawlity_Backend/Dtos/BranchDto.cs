using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Dtos
{
    public class BranchDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int RestaurantId { get; set; }
    }

    public class CreateBranchDto
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public int RestaurantId { get; set; }
    }
    public class BranchProfile : Profile
    {
        public BranchProfile()
        {
            CreateMap<Branch, BranchDto>().ReverseMap();
            CreateMap<CreateBranchDto, Branch>();
        }
    }

}
