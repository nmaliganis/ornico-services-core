using System;

namespace ornico.common.infrastructure.Exceptions.Repositories.Products
{
  public class MultipleProductsForAnIdException : Exception
  {
    private readonly Guid _productId;

    public MultipleProductsForAnIdException(Guid id)
    {
      this._productId = id;
    }

    public override string Message => $"Multiple Products found for: {_productId}";
  }
}
