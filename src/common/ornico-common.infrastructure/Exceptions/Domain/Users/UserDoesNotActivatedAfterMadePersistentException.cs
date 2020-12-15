using System;

namespace ornico.common.infrastructure.Exceptions.Domain.Users
{
    public class UserDoesNotActivatedAfterMadePersistentException : Exception
    {
        public string Login { get; private set; }

        public UserDoesNotActivatedAfterMadePersistentException(string login)
        {
            Login = login;
        }

        public override string Message => $" User with Login: {Login} was not Activated!";
    }
}
