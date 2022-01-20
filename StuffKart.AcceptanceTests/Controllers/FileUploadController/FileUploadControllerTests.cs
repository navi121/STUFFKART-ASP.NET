using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using System.IO;
using FluentAssertions;
using System.Net;

namespace StuffKartProject.AcceptanceTests.Controllers.FileUploadController
{
  public class FileUploadControllerTests : FileUploadControllerTestBase
  {
    private readonly AutoFixture.Fixture _fixture;

    public FileUploadControllerTests()
    {
      _fixture = new AutoFixture.Fixture();
    }

    [Fact]
    public async Task FileUpload_Controller_Returns_200Ok()
    {
      var uploadRequest = _fixture.Create<UploadProducts>();
      List<IFormFile> testFormFiles = new List<IFormFile>();
      IFormFile formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 0, "Data", "image.png"); 
      testFormFiles.Add(formFile);

      var response = PostImageIntoProductId(uploadRequest.ProductId, testFormFiles);
      var result = await HttpClient.SendAsync(response);

      result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task FileUpload_Controller_Returns_500InternalServer_Error()
    {
      var request = PostImageIntoProductId(1);
      var result = await HttpClient.SendAsync(request);

      result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
  }
}
