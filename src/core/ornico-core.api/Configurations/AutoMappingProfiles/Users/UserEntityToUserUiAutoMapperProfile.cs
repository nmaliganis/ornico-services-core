using AutoMapper;
using ornico.common.dtos.DTOs.Users;
using ornico.core.model.Users;

namespace ornico.core.api.Configurations.AutoMappingProfiles.Users
{
    public class UserEntityToUserUiAutoMapperProfile : Profile
    {
        public UserEntityToUserUiAutoMapperProfile()
        {
            ConfigureMapping();
        }

        public void ConfigureMapping()
        {
            CreateMap<User, UserUiModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.CurrentDate, opt => opt.MapFrom(src => src.CurrentDate))
                .MaxDepth(1)
                ;
        }
    }
}