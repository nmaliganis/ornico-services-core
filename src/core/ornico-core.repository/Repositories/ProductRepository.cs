using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using ornico.core.model.Products;
using ornico.core.repository.ContractRepositories;
using ornico.core.repository.Repositories.Base;

namespace ornico.core.repository.Repositories
{
  public class ProductRepository : RepositoryBase<Product, Guid>, IProductRepository
  {
    public ProductRepository(ISession session)
      : base(session)
    {
    }

    //public QueryResult<Product> FindAllProductsPagedOf(int? pageNum = -1, int? pageSize = -1)
    //{
    //  var query = Session.QueryOver<Product>();

    //  if (pageNum == -1 & pageSize == -1)
    //  {
    //    return new QueryResult<Product>(query?
    //      .Where(e=>e.IsActive &&  e.RoleName == null)
    //      .List()
    //      .AsQueryable());
    //  }

    //  return new QueryResult<Product>(query
    //        .Where(e=>e.IsActive &&  e.RoleName == null)
    //        .Skip(ResultsPagingUtility.CalculateStartIndex((int) pageNum, (int) pageSize))
    //        .Take((int) pageSize).List().AsQueryable(),
    //      query.ToRowCountQuery().RowCount(),
    //      (int) pageSize)
    //    ;
    //}

    public Product FindByFirstNameAndLastName(string firstName, string lastName)
    {
      return (Product)
        Session.CreateCriteria(typeof(Product))
          .Add(Expression.Eq("FirstName", firstName))
          .Add(Expression.Eq("Name", lastName))
          .UniqueResult()
        ;
    }

    public IList<Product> FindActiveProducts(bool active)
    {
      return
        Session.CreateCriteria(typeof(Product))
          .Add(Expression.Eq("Active", active))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .List<Product>()
        ;
    }

    public Product FindProductByEmail(string email)
    {
      return
        (Product)
        Session.CreateCriteria(typeof(Product))
          .Add(Expression.Eq("Name", email))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .UniqueResult()
        ;
    }

    public Product FindProductByUserId(Guid userId)
    {
      return
        (Product)
        Session.CreateCriteria(typeof(Product))
          .Add(Expression.Eq("UserId", userId))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .UniqueResult()
        ;
    }

    public IList<Product> FindProductByEmailOrLogin(string email, string login)
    {
      return
        Session.CreateCriteria(typeof(Product))
          .CreateAlias("Tenant", "t", JoinType.InnerJoin)
          .Add(Restrictions.Or(
            Restrictions.Eq("Name", email),
            Restrictions.Eq("t.Login", login)))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .List<Product>()
        ;
    }

    public IList<Product> FindProductsForRoutes()
    {
      return
        Session.CreateCriteria(typeof(Product))
          .CreateAlias("ProductRole", "r", JoinType.InnerJoin)
          .Add(Restrictions.Eq("IsActive", true))
          //.Add(Restrictions.Ge("r.Type", ProductRoleType.Driver))
          //.Add(Restrictions.Le("r.Type", ProductRoleType.Cleaner))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .List<Product>()
        ;
    }

    public Product FindOneBy(Guid ProductId)
    {
      return
        (Product)
        Session.CreateCriteria(typeof(Product))
          .Add(Expression.Eq("Id", ProductId))
          .Add(Expression.Eq("IsActive", true))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .UniqueResult()
        ;
    }
  }
}
