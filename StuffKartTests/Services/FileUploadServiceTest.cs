using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class FileUploadServiceTest
  {
    private readonly StuffKartContext context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<FileUploadService>> _logger;
    public FileUploadServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<FileUploadService>>();
    }

    [Fact]
    public async Task Given_Valid_FileUpload_Service_SaveIn_Db()
    {
      //Arrange
      var uploadRequest = randomProductDetail();
      context.Products.Add(uploadRequest);
      await context.SaveChangesAsync();
      List<IFormFile> testFormFiles = new List<IFormFile>();
      IFormFile formFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 0, "Data", "image.png");
      testFormFiles.Add(formFile);
      var service = new FileUploadService(context,_logger.Object);

      //Act
      service.ImageUpload(uploadRequest.ProductId, testFormFiles);
    }

    private UploadProducts randomProductDetail()
    {
      return _fixture.Create<UploadProducts>();
    }
  }
}
