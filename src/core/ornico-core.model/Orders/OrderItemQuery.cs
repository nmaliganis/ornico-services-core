using ornico.core.model.Products;

namespace ornico.core.model.Orders
{
  public class OrderItemQuery
  {
    public OrderItem OrderItem { get; set; }
    public Order Order { get; set; }
    public Product Product { get; set; }
  }
}