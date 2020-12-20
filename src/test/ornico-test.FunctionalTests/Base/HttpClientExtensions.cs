using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace ornico.test.functional.Base
{
  public static class HttpClientExtensions
  {
    public static HttpClient CreateIdempotentClient(this TestServer server, string token = null)
    {
      var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

      var client = server.CreateClient();

      client.BaseAddress = new Uri(config["AppApiBaseUrl"]);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      if(token != null)
        client.DefaultRequestHeaders.Authorization =
          new AuthenticationHeaderValue(token);

      return client;
    }
  }

}
