using FluentNHibernate.Mapping;
using ornico.core.model.Orders;
using ornico.core.model.Products;

namespace ornico.core.repository.Mappings.Orders
{
  public class OrderItemMap : ClassMap<OrderItem>
  {
    public OrderItemMap()
    {
      Schema(@"public");
      Table(@"`order-items`");
      LazyLoad();
      Id(x => x.Id)
        .Column("id")
        .CustomType("Guid")
        .Access.Property().CustomSqlType("uuid")
        .Not.Nullable()
        .GeneratedBy
        .GuidComb()
        ;

      Map(x => x.InsertedDate)    
        .Column("inserteddate")
        .CustomType("DateTime")
        .Access.Property()
        .Generated.Never()
        .Default(@"now()").CustomSqlType("timestamp")
        .Not.Nullable()
        ;

      Map(x => x.Quantity)    
        .Column("quantity")
        .CustomType("Int32")
        .Access.Property()
        .Generated.Never()
        .Default(@"0").CustomSqlType("int4")
        .Not.Nullable()
        ;

      References(x => x.Product)
        .Class<Product>()
        .Access.Property()
        .Cascade.None()
        .LazyLoad()
        .Columns("product_id")
        ;

      References(x => x.Order)
        .Class<Order>()
        .Access.Property()
        .Cascade.None()
        .LazyLoad()
        .Columns("order_id")
        ;

    }
  }
}
