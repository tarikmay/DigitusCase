using AutoMapper;
using UserLoginApp.DTO.DTOs.UserDTOs;
using UserLoginApp.Entities.Conrete;

namespace UserLoginApp.API.Mapping
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<LoginDto, User>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<ForgotPasswordDto, User>();
        }
    }
}
