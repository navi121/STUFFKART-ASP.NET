using Newtonsoft.Json;
using StuffKartProject.AcceptanceTests.Constants;
using StuffKartProject.AcceptanceTests.Helpers;
using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace StuffKartProject.AcceptanceTests.Controllers.CartDetailsController
{
  public class CartDetailsControllerTestBase : TestBase
  {
    public CartDetailsControllerTestBase()
    {

    }
    protected HttpRequestMessage MakeRequestAsync(string userEmail=null,CartDetail cartRequest=null)
    {
      var requestPath = $"http://localhost/DivideCategory/{userEmail}";
      var postRequest = new HttpRequestMessage(HttpMethod.Post, requestPath);

      StringContent requestPayload;
      if (cartRequest == null)
      {
        requestPayload = new StringContent(string.Empty);
      }
      else
      {
        requestPayload = new StringContent(JsonConvert.SerializeObject(cartRequest),
          Encoding.UTF8, MediaTypeNames.Application.Json);
      }
      postRequest.Content = requestPayload;
      postRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

      return postRequest;
    }
  }
}
