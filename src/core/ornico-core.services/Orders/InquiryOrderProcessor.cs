using System;
using System.Threading.Tasks;
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

        //public Task<OrderUiModel> GetOrderByIdAsync(Guid id)
        //{
        //    return Task.Run(() => _autoMapper.Map<OrderUiModel>(_orderRepository.FindBy(id)));
        //}

        //public Task<OrderUiModel> GetOrderByEmailAsync(string email)
        //{
        //    return Task.Run(() => _autoMapper.Map<OrderUiModel>(_orderRepository.FindOrderByEmail(email)));
        //}

        //public Task<bool> SearchIfAnyOrderByEmailOrLoginExistsAsync(string email, string login)
        //{
        //    return Task.Run(() =>  _orderRepository.FindOrderByEmailOrLogin(email, login).Count > 0);
        //}
        public Task<bool> SearchIfAnyOrderByLastNameOrFirstNameExistsAsync(string lastName, string firstName)
        {
          throw new NotImplementedException();
        }

        public Task<int> GetOrderCountTotalsAsync()
        {
          throw new NotImplementedException();
        }

        public Task<object> GetOrderByIdAsync(Guid id)
        {
          throw new NotImplementedException();
        }
    }
}