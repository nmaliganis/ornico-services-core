using System;
using System.Collections.Generic;
using ornico.core.model.Products;

namespace ornico.core.model.Orders
{
  public class OrderQuery
  {
    public Guid Id { get; set; }
    public double Totals { get; set; }
    public List<ProductQuery> Products { get; set; }
  }
}