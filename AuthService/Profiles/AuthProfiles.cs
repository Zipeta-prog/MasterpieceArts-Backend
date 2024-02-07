using AuthService.Models.Dto;
using AuthService.Models;
using AutoMapper;

namespace AuthService.Profiles
{
    public class AuthProfiles : Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterUserDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(r => r.Email));

            CreateMap<UserDto, ApplicationUser>().ReverseMap();
        }
    }
}
