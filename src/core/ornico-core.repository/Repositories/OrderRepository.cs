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

    public Order FindByFirstNameAndLastName(string lastName, string firstName)
    {
      return (Order)
        Session.CreateCriteria(typeof(Order))
          .Add(Expression.Eq("FirstName", firstName))
          .Add(Expression.Eq("LastName", lastName))
          .UniqueResult()
        ;
    }

    public IList<Order> FindActiveOrders(bool active)
    {
      return
        Session.CreateCriteria(typeof(Order))
          .Add(Expression.Eq("Active", active))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .List<Order>()
        ;
    }

    public Order FindOneOrderByMobile(string mobile)
    {
      throw new NotImplementedException();
    }

    public Order FindOrderByDeviceId(Guid idDevice)
    {
      throw new NotImplementedException();
    }

    public Order FindOrderByEmail(string email)
    {
      return
        (Order)
        Session.CreateCriteria(typeof(Order))
          .Add(Expression.Eq("Name", email))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .UniqueResult()
        ;
    }

    public Order FindOrderByUserId(Guid userId)
    {
      return
        (Order)
        Session.CreateCriteria(typeof(Order))
          .Add(Expression.Eq("UserId", userId))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .UniqueResult()
        ;
    }

    public IList<Order> FindOrderByEmailOrLogin(string email, string login)
    {
      return
        Session.CreateCriteria(typeof(Order))
          .CreateAlias("Tenant", "t", JoinType.InnerJoin)
          .Add(Restrictions.Or(
            Restrictions.Eq("Name", email),
            Restrictions.Eq("t.Name", login)))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .List<Order>()
        ;
    }

    public IList<Order> FindOrdersForRoutes()
    {
      return
        Session.CreateCriteria(typeof(Order))
          .CreateAlias("OrderRole", "r", JoinType.InnerJoin)
          .Add(Restrictions.Eq("IsActive", true))
          //.Add(Restrictions.Ge("r.Type", OrderRoleType.Driver))
          //.Add(Restrictions.Le("r.Type", OrderRoleType.Cleaner))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .List<Order>()
        ;
    }

    public Order FindOneBy(Guid OrderId)
    {
      return
        (Order)
        Session.CreateCriteria(typeof(Order))
          .Add(Expression.Eq("Id", OrderId))
          .Add(Expression.Eq("IsActive", true))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .UniqueResult()
        ;
    }

    public int FindCountTotals()
    {
      int count = 0;
      try
      {
        count = Session
          .CreateCriteria<NotImplementedException>()
          .Add(Expression.Eq("IsActive", true))
          .SetProjection(
            Projections.Count(Projections.Id())
          )
          .UniqueResult<int>();
      }
      catch (Exception e)
      {
        Log.Error(
          $"FindCountTotals" + $"Error Message:{e.Message}" + 
          "--OrderRepository--  @fail@ [OrderRepository]." + $" @inner-fault:{e?.Message} and {e?.InnerException}");
      }

      return count;
    }
  }
}
