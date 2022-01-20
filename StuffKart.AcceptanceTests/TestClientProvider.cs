using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StuffKartProject.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;

namespace StuffKartProject.AcceptanceTests
{
  public class TestClientProvider
  {
    public HttpClient Client { get; private set; }

    public TestClientProvider()
    {      
      var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

      Client = server.CreateClient();
    }
    
  }
}




//var appfactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
//{
//  builder.ConfigureServices(services =>
//  {
//    services.RemoveAll(typeof(StuffKartContext));
//    services.AddDbContext<StuffKartContext>(options => { options.UseInMemoryDatabase("TestDb"); });
//  });
//});





//protected async Task AuthenticateAsync()
//{
//  TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
//}

//private async Task<string> GetJwtAsync()
//{
//  var response = await Client.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequest
//  {
//    Email = "test@integration.com",
//    Password = "SomePass1234!"
//  });

//  var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
//  return registrationResponse.Token;
//  //}
//}
