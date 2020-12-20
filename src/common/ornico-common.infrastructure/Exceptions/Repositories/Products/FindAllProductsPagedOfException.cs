using System;

namespace ornico.common.infrastructure.Exceptions.Repositories.Products
{
  public class FindAllProductsPagedOfException : Exception
  {
    private readonly string _messageDetails;

    public FindAllProductsPagedOfException(string messageDetails)
    {
      this._messageDetails = messageDetails;
    }

    public override string Message => $"Find all Products error: {_messageDetails}";
  }
}
