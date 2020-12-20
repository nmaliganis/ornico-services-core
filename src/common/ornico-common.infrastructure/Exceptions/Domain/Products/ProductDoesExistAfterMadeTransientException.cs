using System;

namespace ornico.common.infrastructure.Exceptions.Domain.Products
{
  public class ProductDoesExistAfterMadeTransientException : Exception
  {
    public Guid Id { get; }

    public ProductDoesExistAfterMadeTransientException(Guid id)
    {
      Id = id;
    }
    public override string Message => $" Product with Id: {Id} was not made Transient!";
  }
}