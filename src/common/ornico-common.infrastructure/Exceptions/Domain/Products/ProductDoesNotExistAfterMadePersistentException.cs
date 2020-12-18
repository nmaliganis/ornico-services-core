using System;

namespace ornico.common.infrastructure.Exceptions.Domain.Products
{
    public class ProductDoesNotExistAfterMadePersistentException : Exception
    {
        public string Name { get; private set; }

        public ProductDoesNotExistAfterMadePersistentException(string name)
        {
            Name = name;
        }

        public override string Message => $" Product with Name: {Name} was not made Persistent!";
    }
}