using System;
using ornico.common.infrastructure.Domain;
using ornico.core.model.Users;

namespace ornico.core.repository.ContractRepositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        User FindUserByLogin(string login);
    }
}