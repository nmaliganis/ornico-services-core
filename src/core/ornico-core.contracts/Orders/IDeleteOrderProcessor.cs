using System;
using System.Threading.Tasks;
using ornico.common.dtos.DTOs.Orders;

namespace ornico.core.contracts.Orders
{
  public interface IDeleteOrderProcessor
  {
    Task<bool> DeleteOrderAsync(Guid id);
  }
}