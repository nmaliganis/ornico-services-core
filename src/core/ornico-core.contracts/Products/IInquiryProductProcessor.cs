using System;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Products;

namespace ornico.core.contracts.Products
{
    public interface IInquiryProductProcessor
    {
        Task<ProductUiModel> GetProductByIdAsync(Guid id);
    }
}