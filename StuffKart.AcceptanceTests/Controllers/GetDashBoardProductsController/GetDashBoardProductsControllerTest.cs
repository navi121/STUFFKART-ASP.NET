using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartProject.AcceptanceTests.Controllers
{
  public class GetDashBoardProductsControllerTest
  {
    [Fact]
    public async Task Get_All_Products()
    {
      var client = new TestClientProvider().Client;

      var response = await client.GetAsync("/GetProductsDetails");

      response.EnsureSuccessStatusCode();

      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
  }
}
