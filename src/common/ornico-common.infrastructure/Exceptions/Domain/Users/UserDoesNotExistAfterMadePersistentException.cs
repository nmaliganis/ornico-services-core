using System;

namespace ornico.common.infrastructure.Exceptions.Domain.Users
{
    public class UserDoesNotExistAfterMadePersistentException : Exception
    {
        public string Email { get; private set; }
        public string Login { get; private set; }

        public UserDoesNotExistAfterMadePersistentException(string login, string email)
        {
            Email = email;
            Login = login;
        }

        public override string Message => $" User with Name: {Email} and Name:{Login} was not made Persistent!";
    }
}