using AutoMapper;
using ornico.common.dtos.DTOs.Orders;
using ornico.core.model.Orders;

namespace ornico.core.api.Configurations.AutoMappingProfiles.Orders
{
  public class OrderEntityToOrderUiAutoMapperProfile : Profile
  {
    public OrderEntityToOrderUiAutoMapperProfile()
    {
      ConfigureMapping();
    }

    public void ConfigureMapping()
    {
      CreateMap<Order, OrderUiModel>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.OrderTotals, opt => opt.MapFrom(src => src.Totals))
        .MaxDepth(1)
        .PreserveReferences()
        ;
            
    }
  }
}