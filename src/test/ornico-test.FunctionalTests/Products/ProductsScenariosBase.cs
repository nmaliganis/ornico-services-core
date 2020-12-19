using System;
using ornico.test.functional.Base;

namespace ornico.test.functional.Products
{
  public class ProductsScenariosBase : BaseServer
  {
    private const string CustomerApiUrlBase = "api/customers";


    public static class Get
    {
      public static string GetCustomer(Guid id)
      {
        return $"{CustomerApiUrlBase}/{id}";
      }

      public static string GetCustomers()
      {
        return $"{CustomerApiUrlBase}";
      }
    }

    public static class Post
    {
      public static string Customer = $"{CustomerApiUrlBase}";
    }
    public static class Put
    {
      public static string Customer(Guid id)
      {
        return $"{CustomerApiUrlBase}/{id}";
      }
    }

    public static class Delete
    {
      public static string Customer(Guid id)
      {
        return $"{CustomerApiUrlBase}/{id}";
      }
    }
  }
}
