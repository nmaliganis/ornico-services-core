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

        public Task DeleteOrderAsync(Guid orderToBeDeletedId)
        {
            throw new NotImplementedException();
        }

        //public Task<OrderForDeletionUiModel> SoftDeleteOrderAsync(Guid userAuditId, Guid id)
        //{
        //  throw new NotImplementedException();
        //}

        //public Task<OrderForDeletionUiModel> HardDeleteOrderAsync(Guid userAuditId, Guid id)
        //{
        //  throw new NotImplementedException();
        //}
    }
}