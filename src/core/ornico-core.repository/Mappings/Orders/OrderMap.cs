using FluentNHibernate.Mapping;
using ornico.core.model.Orders;
using ornico.core.model.Users;

namespace ornico.core.repository.Mappings.Orders
{
  public class OrderMap : ClassMap<Order>
  {
    public OrderMap()
    {
      Schema(@"public");
      Table(@"orders");

      LazyLoad();
      Id(x => x.Id)
        .Column("id")
        .CustomType("Guid")
        .Access.Property().CustomSqlType("uuid")
        .Not.Nullable()
        .GeneratedBy
        .GuidComb()
        ;

      Map(x => x.CreatedDate)    
        .Column("createddate")
        .CustomType("DateTime")
        .Access.Property()
        .Generated.Never()
        .Default(@"now()")
        .CustomSqlType("timestamp")
        .Not.Nullable()
        ;

      Map(x => x.Totals)    
        .Column("totals")
        .CustomType("Double")
        .Access.Property()
        .Generated.Never()
        .Default(@"0.0").CustomSqlType("float8")
        .Not.Nullable()
        ;

      Map(x => x.Comments)    
        .Column("comments")
        .CustomType("String")
        .Access.Property()
        .Generated.Never()
        .CustomSqlType("varchar")
        ;

      References(x => x.User)
        .Class<User>()
        .Access.Property()
        .Cascade.None()
        .LazyLoad()
        .Columns("user_id");

      HasMany<OrderItem>(x => x.Items)
        .Access.Property()
        //.AsSet()
        .Cascade.All()
        .LazyLoad()
        .Inverse()
        .Generic()
        .KeyColumns.Add("order_id", mapping => mapping.Name("order_id")
          .SqlType("uuid")
          .Not.Nullable());
    }
  }
}
