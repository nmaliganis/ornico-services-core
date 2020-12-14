using System;

namespace magic.button.common.infrastructure.Exceptions.Domain.Roles
{
    public class InvalidRoleException : Exception
    {
        public string BrokenRules { get; private set; }

        public InvalidRoleException(string brokenRules)
        {
            BrokenRules = brokenRules;
        }
    }
}