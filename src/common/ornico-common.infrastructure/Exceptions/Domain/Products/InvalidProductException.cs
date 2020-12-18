using System;

namespace ornico.common.infrastructure.Exceptions.Domain.Products
{
    public class InvalidProductException : Exception
    {
        public string BrokenRules { get; private set; }

        public InvalidProductException(string brokenRules)
        {
            BrokenRules = brokenRules;
        }
    }
}
