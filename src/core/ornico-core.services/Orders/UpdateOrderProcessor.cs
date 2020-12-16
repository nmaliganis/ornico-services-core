using ornico.common.infrastructure.TypeMappings;
using ornico.common.infrastructure.UnitOfWorks;
using ornico.core.contracts.Orders;
using ornico.core.repository.ContractRepositories;

namespace ornico.core.services.Orders
{
    public class UpdateOrderProcessor : IUpdateOrderProcessor
    {
        private readonly IUnitOfWork _uOf;
        private readonly IOrderRepository _orderRepository;
        private readonly IAutoMapper _autoMapper;
        public UpdateOrderProcessor(IUnitOfWork uOf, IAutoMapper autoMapper, IOrderRepository orderRepository)
        {
            _uOf = uOf;
            _orderRepository = orderRepository;
            _autoMapper = autoMapper;
        }

        //public Task<OrderUiModel> UpdateOrderAsync(OrderForModificationUiModel updatedOrder)
        //{
        //    throw new NotImplementedException();
        //}
    }
}