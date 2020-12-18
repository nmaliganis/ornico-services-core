using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using ornico.common.infrastructure.Domain.Queries;
using ornico.common.infrastructure.Paging;
using ornico.core.repository.ContractRepositories;
using ornico.core.repository.Repositories.Base;
using Serilog;
using Order = ornico.core.model.Orders.Order;

namespace ornico.core.repository.Repositories
{
  public class OrderRepository : RepositoryBase<Order, Guid>, IOrderRepository
  {
    public OrderRepository(ISession session)
      : base(session)
    {
    }

    public QueryResult<Order> FindAllOrdersPagedOf(int? pageNum = -1, int? pageSize = -1)
    {
      var query = Session.QueryOver<Order>();

      if (pageNum == -1 & pageSize == -1)
      {
        return new QueryResult<Order>(query?
          .List()
          .AsQueryable());
      }

      return new QueryResult<Order>(query
            .Skip(ResultsPagingUtility.CalculateStartIndex((int)pageNum, (int)pageSize))
            .Take((int)pageSize).List().AsQueryable(),
          query.ToRowCountQuery().RowCount(),
          (int)pageSize)
        ;
    }

    public Order FindOrderForUserByOrderId(Guid idUser, Guid idOrder)
    {
      return
        (Order)
        Session.CreateCriteria(typeof(Order))
          //.CreateAlias("User", "u", JoinType.InnerJoin)
          .Add(Expression.Eq("Id", idOrder))
          //.Add(Expression.Eq("u.Id", idUser))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .UniqueResult()
        ;
    }
  }
}
