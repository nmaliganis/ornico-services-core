using AutoMapper;
using ornico.common.dtos.DTOs.Products;
using ornico.core.model.Products;

namespace ornico.core.api.Configurations.AutoMappingProfiles.Products
{
  public class ProductEntityToProductUiAutoMapperProfile : Profile
  {
    public ProductEntityToProductUiAutoMapperProfile()
    {
      ConfigureMapping();
    }

    public void ConfigureMapping()
    {
      CreateMap<Product, ProductUiModel>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
        .MaxDepth(1)
        .PreserveReferences()
        ;
            
    }
  }
}