using System;
using System.Reflection;
using AspNetCoreRateLimit;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Driver;
using NHibernate.Spatial.Dialect;
using NHibernate.Spatial.Mapping;
using NHibernate.Spatial.Metadata;
using ornico.common.infrastructure.Exceptions.Repositories;
using ornico.common.infrastructure.PropertyMappings;
using ornico.common.infrastructure.PropertyMappings.TypeHelpers;
using ornico.common.infrastructure.TypeMappings;
using ornico.common.infrastructure.UnitOfWorks;
using ornico.core.api.Helpers;
using ornico.core.contracts.Orders;
using ornico.core.contracts.Users;
using ornico.core.contracts.V1;
using ornico.core.repository.ContractRepositories;
using ornico.core.repository.Mappings.Users;
using ornico.core.repository.NhUnitOfWork;
using ornico.core.repository.Repositories;
using ornico.core.services.Orders;
using ornico.core.services.Users;
using ornico.core.services.V1;

namespace ornico.core.api.Configurations
{
  public static class Config
  {
    public static void ConfigureRepositories(IServiceCollection services)
    {
      services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
      services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
      services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
      services.AddScoped<IUrlHelper>(implementationFactory =>
      {
        var actionContext = implementationFactory.GetService<IActionContextAccessor>()
          .ActionContext;
        return new UrlHelper(actionContext);
      });

      services.AddScoped<IUrlHelper>(x =>
      {
        var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
        var factory = x.GetRequiredService<IUrlHelperFactory>();
        return factory.GetUrlHelper(actionContext);
      });

      services.AddSingleton<IPropertyMappingService, PropertyMappingService>();
      services.AddSingleton<ITypeHelperService, TypeHelperService>();

      services.AddScoped<IInquiryUserProcessor, InquiryUserProcessor>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IUsersControllerDependencyBlock, UsersControllerDependencyBlock>();

      services.AddScoped<IInquiryOrderProcessor, InquiryOrderProcessor>();
      services.AddScoped<IInquiryAllOrdersProcessor, InquiryAllOrdersProcessor>();
      services.AddScoped<ICreateOrderProcessor, CreateOrderProcessor>();
      services.AddScoped<IUpdateOrderProcessor, UpdateOrderProcessor>();
      services.AddScoped<IDeleteOrderProcessor, DeleteOrderProcessor>();
      services.AddScoped<IOrderRepository, OrderRepository>();
      services.AddScoped<IOrdersControllerDependencyBlock, OrdersControllerDependencyBlock>();
    }

    public static void ConfigureAutoMapper(IServiceCollection services)
    {
      services.AddSingleton<IAutoMapper, AutoMapperAdapter>();
    }

    public static void ConfigureNHibernate(IServiceCollection services, string connectionString)
    {
      try
      {
        var cfg = Fluently.Configure()
          .Database(PostgreSQLConfiguration.PostgreSQL82
            .ConnectionString(connectionString)
            .Driver<NpgsqlDriver>()
            .Dialect<PostGis20Dialect>()
            .ShowSql()
            .MaxFetchDepth(5)
            .FormatSql()
            .Raw("transaction.use_connection_on_system_prepare", "true")
            .AdoNetBatchSize(100)
          )
          .Mappings(x => x.FluentMappings.AddFromAssemblyOf<UserMap>())
          .Cache(c => c.UseSecondLevelCache().UseQueryCache()
            .ProviderClass(typeof(NHibernate.Caches.RtMemoryCache.RtMemoryCacheProvider)
              .AssemblyQualifiedName)
          )
          .CurrentSessionContext("web")
          .BuildConfiguration();

        cfg.AddAssembly(Assembly.GetExecutingAssembly());
        cfg.AddAuxiliaryDatabaseObject(new SpatialAuxiliaryDatabaseObject(cfg));
        Metadata.AddMapping(cfg, MetadataClass.GeometryColumn);
        Metadata.AddMapping(cfg, MetadataClass.SpatialReferenceSystem);

        var sessionFactory = cfg.BuildSessionFactory();

        services.AddSingleton<ISessionFactory>(sessionFactory);

        services.AddScoped<ISession>((ctx) =>
        {
          var sf = ctx.GetRequiredService<ISessionFactory>();

          return sf.OpenSession();

        });

        services.AddScoped<IUnitOfWork, NhUnitOfWork>();
      }
      catch (Exception ex)
      {
        throw new NHibernateInitializationException(ex.Message, ex.InnerException?.Message);
      }
    }
  }
}
