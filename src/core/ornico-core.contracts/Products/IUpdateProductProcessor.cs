using System;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Products;

namespace ornico.core.contracts.Products
{
    public interface IUpdateProductProcessor
    {
        Task<ProductUiModel> UpdateProductAsync(ProductForModificationUiModel updatedProduct);
        //Task<ProductDeviceProvisioningUiModel> ProvisioningDeviceToProductAsync(Guid userAuditId, Guid id, Guid deviceId, 
        //  ProductForModificationProvisioningModel productForModificationProvisioningModel);
    }
}
