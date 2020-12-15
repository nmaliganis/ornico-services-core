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
    Order FindByFirstNameAndLastName(string lastName, string firstName);
    IList<Order> FindActiveOrders(bool active);
    Order FindOneOrderByMobile(string mobile);
    Order FindOrderByDeviceId(Guid idDevice);
    Order FindOneBy(Guid idOrder);
    int FindCountTotals();
  }
}
