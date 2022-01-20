using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class FileUploadServiceTest
  {
    private readonly StuffKartContext _mockContext;
    private readonly FileUploadService _fileUploadService;
    private readonly Fixture _fixture = new Fixture();
    public FileUploadServiceTest()
    {
      _mockContext = new StuffKartContext();
      var _logger = new Mock<ILogger<FileUploadService>>();
      _fileUploadService = new FileUploadService(_mockContext, _logger.Object);
    }

    [Fact]
    public async Task Given_Valid_FileUpload_Service_SaveIn_Db()
    {
      //Arrange
      var uploadRequest = _fixture.Create<UploadProducts>();
      uploadRequest.ProductId = 17;
      IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 0, "Data", "image.png");

      //Act
      _fileUploadService.ImageUpload(uploadRequest.ProductId, file);
    }
  }
}
