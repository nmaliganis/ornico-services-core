using System;
using ornico.test.functional.Base;

namespace ornico.test.functional.Products
{
  public class ProductsScenariosBase : BaseServer
  {
    private const string ProductApiUrlBase = "api/Products";


    public static class Get
    {
      public static string GetProduct(Guid id)
      {
        return $"{ProductApiUrlBase}/{id}";
      }

      public static string GetProducts()
      {
        return $"{ProductApiUrlBase}";
      }
    }

    public static class Post
    {
      public static string Product = $"{ProductApiUrlBase}";
    }
    public static class Put
    {
      public static string Product(Guid id)
      {
        return $"{ProductApiUrlBase}/{id}";
      }
    }

    public static class Delete
    {
      public static string Product(Guid id)
      {
        return $"{ProductApiUrlBase}/{id}";
      }
    }
  }
}
