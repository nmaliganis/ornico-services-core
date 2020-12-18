using AutoMapper;
using ornico.common.dtos.DTOs.Products;
using ornico.core.model.Products;

namespace ornico.core.api.Configurations.AutoMappingProfiles.Products
{
    public class ProductForModificationUiModelToProductEntityAutoMapperProfile : Profile
    {
        public ProductForModificationUiModelToProductEntityAutoMapperProfile()
        {
            ConfigureMapping();
        }

        public void ConfigureMapping()
        {
            CreateMap<ProductForModificationUiModel, Product>()
                .ForMember(dest => dest.Name, opt => 
                  opt.MapFrom(src => src.ProductName))
                .MaxDepth(1)
                .PreserveReferences()
                ;
        }
    }
}