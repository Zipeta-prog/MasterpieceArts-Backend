using ArtProductService.Models;
using ArtProductService.Models.Dto;
using AutoMapper;

namespace ArtProductService.Profiles
{
    public class ArtProfile : Profile
    {
        public ArtProfile()
        {
            CreateMap<AddArtDto, Art>().ReverseMap();
        }
    }
}
