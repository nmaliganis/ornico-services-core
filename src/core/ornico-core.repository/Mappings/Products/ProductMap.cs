using FluentNHibernate.Mapping;
using ornico.core.model.Orders;
using ornico.core.model.Products;

namespace ornico.core.repository.Mappings.Products
{
  public class ProductMap : ClassMap<Product>
  {
    public ProductMap()
    {
      Schema(@"public");
      Table(@"products");
      LazyLoad();
      Id(x => x.Id)
        .Column("id")
        .CustomType("Guid")
        .Access.Property().CustomSqlType("uuid")
        .Not.Nullable()
        .GeneratedBy
        .GuidComb()
        ;

      Map(x => x.Name)    
        .Column("`name`")
        .CustomType("String")
        .Access.Property()
        .Generated.Never().CustomSqlType("varchar(128)")
        .Not.Nullable()
        .Length(128)
        ;

      Map(x => x.Description)    
        .Column("description")
        .CustomType("String")
        .Access.Property()
        .Generated.Never().CustomSqlType("varchar")
        ;

      Map(x => x.CreatedDate)    
        .Column("createddate")
        .CustomType("DateTime")
        .Access.Property()
        .Generated.Never()
        .Default(@"now()").CustomSqlType("timestamp")
        .Not.Nullable()
        ;

      Map(x => x.Price)    
        .Column("price")
        .CustomType("Double")
        .Access.Property()
        .Generated.Never()
        .Default(@"0.0").CustomSqlType("float8")
        .Not.Nullable()
        ;

      HasMany<OrderItem>(x => x.Items)
        .Access.Property()
        .AsSet()
        .Cascade.All()
        .LazyLoad()
        .Inverse()
        .Generic()
        .KeyColumns.Add("product_id", mapping => mapping.Name("product_id")
          .SqlType("uuid")
          .Not.Nullable())
        ;
    }
  }
}
