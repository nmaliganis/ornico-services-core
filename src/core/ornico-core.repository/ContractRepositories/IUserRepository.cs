using System;
using System.Collections.Generic;
using ornico.common.infrastructure.Domain;
using ornico.core.model.Users;

namespace ornico.core.repository.ContractRepositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        User FindUserByUsername(string username);
        User FindUserByUsernameAndPasswordAsync(string username, string password);
        User FindUserByUsernameAndEmail(string username, string email);
        IList<User> FindUsersByEmailOrUsername(string email, string username);
    }
}