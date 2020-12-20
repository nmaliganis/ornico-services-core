using AutoMapper;
using ornico.common.dtos.DTOs.Products;
using ornico.core.model.Orders;

namespace ornico.core.api.Configurations.AutoMappingProfiles.Products
{
  public class OrderItemEntityToProductRetrievalUiModelAutoMapperProfile : Profile
  {
    public OrderItemEntityToProductRetrievalUiModelAutoMapperProfile()
    {
      ConfigureMapping();
    }

    public void ConfigureMapping()
    {
      CreateMap<OrderItem, ProductRetrievalUiModel>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
        .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
        .ForMember(dest => dest.ProductQty, opt => opt.MapFrom(src => src.Quantity))
        .MaxDepth(1)
        .PreserveReferences()
        ;
    }
  }
}