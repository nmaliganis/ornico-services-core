using System;

namespace ornico.common.infrastructure.Exceptions.Domain.Products
{
    public class ProductDoesNotExistAfterMadePersistentException : Exception
    {
      public Guid Id { get; }
      public string Name { get; private set; }

        public ProductDoesNotExistAfterMadePersistentException(string name)
        {
            Name = name;
        }

        public ProductDoesNotExistAfterMadePersistentException(Guid id)
        {
          Id = id;
        }
        public override string Message => $" Product with Name: {Name} or Id: {Id} was not made Persistent!";
    }
}