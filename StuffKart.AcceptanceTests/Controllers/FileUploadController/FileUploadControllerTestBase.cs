using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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

namespace StuffKartProject.AcceptanceTests.Controllers.FileUploadController
{
  public class FileUploadControllerTestBase : TestBase
  {
    public FileUploadControllerTestBase()
    {
    }

    protected HttpRequestMessage PostImageIntoProductId(int id, List<IFormFile> files=null)
    {
      var requestPath = $"http://localhost/UploadImage{id}";
      var postRequest = new HttpRequestMessage(HttpMethod.Post, requestPath);

      StringContent requestPayload;
      if (files == null)
      {
        requestPayload = new StringContent(string.Empty);
      }
      else
      {
        requestPayload = new StringContent(JsonConvert.SerializeObject(files),
          Encoding.UTF8, MediaTypeNames.Application.Json);
      }
      postRequest.Content = requestPayload;
      postRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

      return postRequest;
    }
  }
}
