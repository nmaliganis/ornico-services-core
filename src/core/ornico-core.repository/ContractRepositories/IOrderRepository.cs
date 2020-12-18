using System;
using System.Collections.Generic;
using ornico.common.infrastructure.Domain;
using ornico.common.infrastructure.Domain.Queries;
using ornico.core.model.Orders;

namespace ornico.core.repository.ContractRepositories
{
  public interface IOrderRepository : IRepository<Order, Guid>
  {
    QueryResult<Order> FindAllOrdersPagedOf(int? pageNum, int? pageSize);
    Order FindOrderForUserByOrderId(Guid idUser, Guid idOrder);
  }
}
