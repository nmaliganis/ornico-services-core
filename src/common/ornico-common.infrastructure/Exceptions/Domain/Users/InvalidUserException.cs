using System;

namespace ornico.common.infrastructure.Exceptions.Domain.Users
{
    public class InvalidUserException : Exception
    {
        public string BrokenRules { get; private set; }

        public InvalidUserException(string brokenRules)
        {
            BrokenRules = brokenRules;
        }
    }
}
