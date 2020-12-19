using System;

namespace ornico.core.model.Products
{
    public class ProductQuery
    {
      public Guid Id { get; set; }
      public string Name { get; set; }
      public double Price { get; set; } = 0.0;
      public int Qty { get; set; } = 0;
    }
}

