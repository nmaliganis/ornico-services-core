using System;
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

        public User FindUserByLogin(string login)
        {
            return (User)
                Session.CreateCriteria(typeof(User))
                    .Add(Expression.Eq("IsActive", true))
                    .Add(Expression.Eq("Login", login))
                    .SetCacheable(true)
                    .SetCacheMode(CacheMode.Normal)
                    .SetFlushMode(FlushMode.Never)
                    .UniqueResult()
                ;
        }
    }
}
