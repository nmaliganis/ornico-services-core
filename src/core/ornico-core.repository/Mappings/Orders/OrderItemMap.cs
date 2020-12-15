using FluentNHibernate.Mapping;
using ornico.core.model.Orders;
using ornico.core.model.Products;

namespace ornico.core.repository.Mappings.Orders
{
    public class OrderItemMap : ClassMap<OrderItem>
    {
      public OrderItemMap()
      {
        Table(@"order-items");

        Id(x => x.Id)
          .Column("id")
          .CustomType("Guid")
          .Access.Property()
          .CustomSqlType("uuid")
          .Not.Nullable()
          .GeneratedBy
          .GuidComb()
          ;

        Map(x => x.Quantity)
          .Column("quantity")
          .Access.Property()
          .Generated.Never()
          .Default(@"0")
          .CustomSqlType("integer")
          .Not.Nullable()
          ;

        Map(x => x.InsertedDate)
          .Column("insertedDate")
          .CustomType("DateTime")
          .Access.Property()
          .Generated.Never()
          .Not.Nullable()
          ;

        References(x => x.Order)
          .Class<Order>()
          .Access.Property()
          .Cascade.SaveUpdate()
          .LazyLoad()
          .Columns("order_id")
          ;

        References(x => x.Product)
          .Class<Product>()
          .Access.Property()
          .Cascade.SaveUpdate()
          .LazyLoad()
          .Columns("product_id")
          ;
      }
    }
}
