using System;

namespace ornico.common.infrastructure.Exceptions.Domain.Products
{
    public class ProductAlreadyExistsException : Exception
    {
        public string Name { get; }
        public string BrokenRules { get; }

        public ProductAlreadyExistsException(string name, string brokenRules)
        {
            Name = name;
            BrokenRules = brokenRules;
        }

        public override string Message => $" Product with name:{Name} already Exists!\n Additional info:{BrokenRules}";
    }
}
