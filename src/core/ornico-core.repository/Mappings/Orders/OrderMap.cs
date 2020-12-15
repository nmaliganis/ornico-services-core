using FluentNHibernate.Mapping;
using ornico.core.model.Orders;
using ornico.core.model.Users;

namespace ornico.core.repository.Mappings.Orders
{
  public class OrderMap : ClassMap<Order>
  {
    public OrderMap()
    {
      Table(@"orders");

      Id(x => x.Id)
        .Column("id")
        .CustomType("Guid")
        .Access.Property()
        .CustomSqlType("uuid")
        .Not.Nullable()
        .GeneratedBy
        .GuidComb()
        ;

      Map(x => x.Totals)
        .Column("totals")
        .CustomType("double")
        .Access.Property()
        .Generated.Never()
        .Nullable()
        ;

      Map(x => x.Comments)
        .Column("comments")
        .CustomType("string")
        .Access.Property()
        .Generated.Never()
        .CustomSqlType("varchar")
        .Nullable()
        ;

      Map(x => x.CreatedDate)
        .Column("createdDate")
        .CustomType("DateTime")
        .Access.Property()
        .Generated.Never()
        .Not.Nullable()
        ;

      References(x => x.User)
        .Class<User>()
        .Access.Property()
        .Cascade.SaveUpdate()
        .LazyLoad()
        .Columns("user_id")
        ;

      HasMany<OrderItem>(x => x.Items)
        .Access.Property()
        .AsSet()
        .Cascade.None()
        .LazyLoad()
        .Inverse()
        .Generic()
        .KeyColumns.Add("order_id", mapping => mapping.Name("order_id")
          .SqlType("uuid")
          .Not.Nullable());
    }
  }
}
