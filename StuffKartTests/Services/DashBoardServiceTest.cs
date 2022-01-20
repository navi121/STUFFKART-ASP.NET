using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class DashBoardServiceTest
  {
    private readonly StuffKartContext _mockContext;
    private readonly DashBoardService _dashBoardService;
    private readonly Fixture _fixture = new Fixture();
    public DashBoardServiceTest()
    {
      _mockContext = new StuffKartContext();
      var _logger = new Mock<ILogger<DashBoardService>>();
      _dashBoardService = new DashBoardService(_mockContext, _logger.Object);
    }

    [Fact]
    public async Task Given_Valid_Products_Details_To_Service_Returns_IndividualProductId()
    {
      //Arrange
      var productDetails = _fixture.Create<UploadProducts>();
      productDetails.ProductId = 0;

      //Act
      var result = _dashBoardService.AddProductDetailService(productDetails);

      //Assert
      Assert.NotEqual(productDetails.ProductId,result.Result);
    }
  }
}
