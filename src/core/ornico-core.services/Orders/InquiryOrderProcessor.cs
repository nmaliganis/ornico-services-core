using System;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Orders;
using ornico.common.infrastructure.TypeMappings;
using ornico.core.contracts.Orders;
using ornico.core.repository.ContractRepositories;

namespace ornico.core.services.Orders
{
  public class InquiryOrderProcessor : IInquiryOrderProcessor
  {
    private readonly IAutoMapper _autoMapper;
    private readonly IOrderRepository _orderRepository;

    public InquiryOrderProcessor(IOrderRepository orderRepository, IAutoMapper autoMapper)
    {
      _orderRepository = orderRepository;
      _autoMapper = autoMapper;
    }
    public Task<OrderUiModel> GetOrderByIdAsync(Guid id)
    {
      return Task.Run(() => _autoMapper.Map<OrderUiModel>(_orderRepository.FindBy(id)));
    }
  }
}