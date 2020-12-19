using System;

namespace ornico.common.infrastructure.Exceptions.Domain.Products
{
    public class FindAllProductsForOrderException : Exception
    {
      public string Error { get; }
      public Guid IdUser { get; }
      public Guid IdOrder { get; }
      public string Name { get; private set; }

      public FindAllProductsForOrderException(string error, Guid idUser, Guid idOrder)
      {
        Error = error;
        IdUser = idUser;
        IdOrder = idOrder;
      }

      public override string Message => $" Find all Products for Order:{IdOrder} for User:{IdUser} failed.";
    }
}
