using FluentNHibernate.Mapping;
using ornico.core.model.Orders;
using ornico.core.model.Users;

namespace ornico.core.repository.Mappings.Users
{
  public class UserMap : ClassMap<User>
  {
    public UserMap()
    {
      Table(@"users");

      Id(x => x.Id)
        .Column("id")
        .CustomType("Guid")
        .Access.Property()
        .CustomSqlType("uuid")
        .Not.Nullable()
        .Precision(11)
        .GeneratedBy
        .GuidComb()
        ;

      Map(x => x.DisplayName)
        .Column("displayname")
        .CustomType("String")
        .Access.Property()
        .Generated.Never()
        .CustomSqlType("varchar(256)")
        .Not.Nullable()
        .Length(256)
        ;

      Map(x => x.UserName)
        .Column("username")
        .CustomType("String")
        .Unique()
        .Access.Property()
        .Generated.Never()
        .CustomSqlType("varchar(256)")
        .Not.Nullable()
        .Length(256)
        ;

      Map(x => x.Password)
        .Column("password")
        .CustomType("String")
        .Unique()
        .Access.Property()
        .Generated.Never()
        .CustomSqlType("varchar")
        .Not.Nullable()
        ;

      Map(x => x.Email)
        .Column("email")
        .CustomType("String")
        .Unique()
        .Access.Property()
        .Generated.Never()
        .CustomSqlType("varchar(128)")
        .Not.Nullable()
        .Length(128)
        ;


      Map(x => x.CreatedDate)
        .Column("createddate")
        .CustomType("DateTime")
        .Access.Property()
        .Generated.Never()
        .Not.Nullable()
        ;

      Map(x => x.CurrentDate)
        .Column("currentdate")
        .CustomType("DateTime")
        .Access.Property()
        .Generated.Never()
        .Not.Nullable()
        ;

      HasMany<Order>(x => x.Orders)
        .Access.Property()
        .AsSet()
        .Cascade.None()
        .LazyLoad()
        .Inverse()
        .Generic()
        .KeyColumns.Add("user_id", mapping => mapping.Name("user_id")
          .SqlType("uuid")
          .Nullable());
    }
  }
}
