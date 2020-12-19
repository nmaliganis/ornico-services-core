using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using ornico.core.api;

namespace ornico.test.functional.Base
{
  public abstract class BaseServer
  {
    public TestServer CreateServer()
    {
      var path = Assembly.GetAssembly(typeof(BaseServer))
        .Location;

      var hostBuilder = new WebHostBuilder()
        .UseContentRoot(Path.GetDirectoryName(path))
        .ConfigureAppConfiguration(cb =>
        {
          cb.AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();
        })
        .UseStartup<Startup>();


      var testServer = new TestServer(hostBuilder);

      return testServer;
    }
  }
}
