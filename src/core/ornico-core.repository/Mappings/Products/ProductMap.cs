using FluentNHibernate.Mapping;
using ornico.core.model.Orders;
using ornico.core.model.Products;

namespace ornico.core.repository.Mappings.Products
{
  public class ProductMap : ClassMap<Product>
  {
    public ProductMap()
    {
      Table(@"products");

      Id(x => x.Id)
        .Column("id")
        .CustomType("Guid")
        .Access.Property()
        .CustomSqlType("uuid")
        .Not.Nullable()
        .GeneratedBy
        .GuidComb()
        ;

      Map(x => x.Name)
        .Column("name")
        .CustomType("string")
        .Access.Property()
        .Generated.Never()
        .CustomSqlType("varchar(128)")
        .Not.Nullable()
        .Length(128)
        ;

      Map(x => x.Description)
        .Column("description")
        .CustomType("string")
        .Access.Property()
        .Generated.Never()
        .CustomSqlType("varchar(128)")
        .Not.Nullable()
        ;

      Map(x => x.CreatedDate)
        .Column("createdDate")
        .CustomType("DateTime")
        .Access.Property()
        .Generated.Never()
        .Not.Nullable()
        ;

      Map(x => x.Price)
        .Column("price")
        .CustomType("double")
        .Access.Property()
        .Generated.Never()
        .Nullable()
        ;

      HasMany<OrderItem>(x => x.Items)
        .Access.Property()
        .AsSet()
        .Cascade.None()
        .LazyLoad()
        .Inverse()
        .Generic()
        .KeyColumns.Add("product_id", mapping => mapping.Name("product_id")
          .SqlType("uuid")
          .Nullable());
    }
  }
}
