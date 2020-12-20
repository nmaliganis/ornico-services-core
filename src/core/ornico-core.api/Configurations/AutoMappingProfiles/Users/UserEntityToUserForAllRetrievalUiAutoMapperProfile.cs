using AutoMapper;
using ornico.common.dtos.DTOs.Users;
using ornico.core.model.Users;

namespace ornico.core.api.Configurations.AutoMappingProfiles.Users
{
    public class UserEntityToUserForRetrievalUiAutoMapperProfile : Profile
    {
        public UserEntityToUserForRetrievalUiAutoMapperProfile()
        {
            ConfigureMapping();
        }

        public void ConfigureMapping()
        {
            CreateMap<User, UserForRetrievalUiModel>()
                .ForMember(dest => dest.Id, opt => opt
                    .MapFrom(src => src.Id))
                .ForMember(dest => dest.Login, opt => opt
                    .MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt
                  .MapFrom(src => src.Email))
                .MaxDepth(1)
                ;
        }
    }
}