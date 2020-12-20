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
    public Product FindOneProductByMobile(string name)
    {
      return
        (Product)
        Session.CreateCriteria(typeof(Product))
          .Add(Expression.Eq("Name", name))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .UniqueResult()
        ;
    }
  }
}
