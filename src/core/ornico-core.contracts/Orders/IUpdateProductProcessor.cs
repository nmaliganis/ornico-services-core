using System;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Orders;

namespace ornico.core.contracts.Orders
{
    public interface IUpdateOrderProcessor
    {
        Task<OrderUiModel> UpdateOrderAsync(OrderForModificationUiModel updatedOrder);
        Task<OrderDeviceProvisioningUiModel> ProvisioningDeviceToOrderAsync(Guid userAuditId, Guid id, Guid deviceId, 
          OrderForModificationProvisioningModel orderForModificationProvisioningModel);
    }
}
