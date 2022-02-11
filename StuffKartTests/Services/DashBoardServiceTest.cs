using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class DashBoardServiceTest
  {
    private readonly StuffKartContext context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<DashBoardService>> _logger;
    public DashBoardServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<DashBoardService>>();
    }

    [Fact]
    public void Given_Valid_Products_Details_To_Service_Returns_IndividualProductId()
    {
      //Arrange
      var productDetails = randomProductDetail();
      var service = new DashBoardService(context,_logger.Object);
      
      //Act
      var result = service.AddProductDetailService(productDetails);

      //Assert
      Assert.Equal(productDetails.ProductId,result.Result);
    }
    private UploadProducts randomProductDetail()
    {
      return _fixture.Create<UploadProducts>();
    }
  }
}
