using Newtonsoft.Json;
using StuffKartProject.AcceptanceTests.Helpers;
using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StuffKartProject.AcceptanceTests.Controllers.CategoryController
{
  public class CategoryControllerTestBase : TestBase
  {

    public string RequestBody(UploadProducts request)
    {
      request.Category = "Men";

      return JsonConvert.SerializeObject(request);
    } 

    public Task<HttpResponseMessage> MakeRequestAsync(UploadProducts categoryName=null)
    {
      var requestPath = $"http://localhost/DivideCategory/{categoryName.Category}";
      var getRequest = new HttpRequestMessage(HttpMethod.Get, requestPath);

      getRequest.Headers.Authorization = new AuthenticationHeaderValue(Jwt);

      return HttpClient.SendAsync(getRequest);
    }
  }
}
