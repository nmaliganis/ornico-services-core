using System;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Orders;

namespace ornico.core.contracts.Orders
{
    public interface IInquiryOrderProcessor
    {
        //Task<OrderUiModel> GetOrderByIdAsync(Guid id);
        //Task<OrderUiModel> GetOrderByMobileAsync(string mobile);
        Task<bool> SearchIfAnyOrderByLastNameOrFirstNameExistsAsync(string lastName, string firstName);
        Task<int> GetOrderCountTotalsAsync();
        Task<object> GetOrderByIdAsync(Guid id);
    }
}