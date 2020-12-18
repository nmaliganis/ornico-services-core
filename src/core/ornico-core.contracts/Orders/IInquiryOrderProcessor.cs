using System;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Orders;

namespace ornico.core.contracts.Orders
{
    public interface IInquiryOrderProcessor
    {
        Task<OrderUiModel> GetOrderByIdAsync(Guid idUser, Guid id);
    }
}