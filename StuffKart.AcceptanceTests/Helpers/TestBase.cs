using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Moq;
using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StuffKartProject.AcceptanceTests.Helpers
{
  public class TestBase
  {
    public const string TestCollectionName = "StuffKartTestCollection";

    public string Jwt { get; }
    public string ConnectionString { get; private set; }
    public IWebHostBuilder HostBuilder { get; }
    public TestServer TestingServer { get; }
    public HttpClient HttpClient { get; private set; }
    
    public TestBase()
    {
      Jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6Im5hdmVlbmNocHRAZ21haWwuY29tIiwibmJmIjoxNjQxODIyOTM1LCJleHAiOjE2NDE4MjY1MzQsImlhdCI6MTY0MTgyMjkzNSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdC5jb20iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0LmNvbSJ9.lmbehn2222uXM5y6lIMPlLoSdVknzQoWXMWwE-5m0jc";

      var server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
      HttpClient = server.CreateClient();

      //var builder = new WebHostBuilder().UseStartup<TestStartup>();
      //TestServer testServer = new TestServer(builder);
      //HttpClient client = testServer.CreateClient();

    }

    public TestBase(IWebHostBuilder builder)
    {
      if (TestingServer != null) return;
      HostBuilder = builder;

      //HostBuilder.UseStartup<Startup>();
      TestingServer = new TestServer(HostBuilder);
      HttpClient = TestingServer.CreateClient();
    }
  }
}
