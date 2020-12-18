using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using ornico.core.model.Users;
using ornico.core.repository.ContractRepositories;
using ornico.core.repository.Repositories.Base;

namespace ornico.core.repository.Repositories
{
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        public UserRepository(ISession session)
            : base(session)
        {
        }

        public User FindUserByUsername(string username)
        {
            return (User)
                Session.CreateCriteria(typeof(User))
                    .Add(Expression.Eq("UserName", username))
                    .SetCacheable(true)
                    .SetCacheMode(CacheMode.Normal)
                    .SetFlushMode(FlushMode.Never)
                    .UniqueResult()
                ;
        }

        public User FindUserByUsernameAndPasswordAsync(string username, string password)
        {
          return
            (User)
            Session.CreateCriteria(typeof(User))
              .Add(Expression.Eq("UserName", username))
              .Add(Expression.Eq("Password", password))
              .SetCacheable(true)
              .SetCacheMode(CacheMode.Get)
              .SetFlushMode(FlushMode.Never)
              .UniqueResult()
            ;
        }

        public User FindUserByUsernameAndEmail(string username, string email)
        {
          return
            (User)
            Session.CreateCriteria(typeof(User))
              .Add(Expression.Eq("UserName", username))
              .Add(Expression.Eq("Email", email))
              .SetCacheable(true)
              .SetCacheMode(CacheMode.Get)
              .SetFlushMode(FlushMode.Never)
              .UniqueResult()
            ;
        }

    public IList<User> FindUsersByEmailOrUsername(string email, string username)
    {
      return
        Session.CreateCriteria(typeof(User))
          .Add(Restrictions.Or(
            Restrictions.Eq("Email", email),
            Restrictions.Eq("UserName", username)))
          .SetCacheable(true)
          .SetCacheMode(CacheMode.Normal)
          .SetFlushMode(FlushMode.Never)
          .List<User>()
        ;
    }
  }
}
