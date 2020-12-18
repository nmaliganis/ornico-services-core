using System;
using System.Threading.Tasks;
using ornico.common.infrastructure.UnitOfWorks;
using ornico.core.contracts.Orders;
using ornico.core.repository.ContractRepositories;

namespace ornico.core.services.Orders
{
  public class DeleteOrderProcessor : IDeleteOrderProcessor
  {
    private readonly IUnitOfWork _uOf;
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderProcessor(IUnitOfWork uOf,
      IOrderRepository orderRepository)
    {
      _uOf = uOf;
      _orderRepository = orderRepository;
    }

    public Task<bool> DeleteOrderAsync(Guid orderToBeDeletedId)
    {
      throw new NotImplementedException();
    }
  }
}