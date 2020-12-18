using AutoMapper;
using ornico.common.dtos.DTOs.Products;
using ornico.core.model.Products;

namespace ornico.core.api.Configurations.AutoMappingProfiles.Products
{
  public class ProductForCreationUiModelToProductEntityAutoMapperProfile : Profile
  {
    public ProductForCreationUiModelToProductEntityAutoMapperProfile()
    {
      ConfigureMapping();
    }

    public void ConfigureMapping()
    {
      CreateMap<ProductForCreationUiModel, Product>()
        .ForMember(dest => dest.Name, opt => 
          opt.MapFrom(src => src.ProductName))
        .MaxDepth(1)
        .PreserveReferences()
        ;
    }
  }
}