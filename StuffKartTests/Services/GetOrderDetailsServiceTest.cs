using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class GetOrderDetailsServiceTest
  {
    private readonly StuffKartContext _mockContext;
    private readonly GetOrderDetailsService _orderDetailsService;
    private readonly Fixture _fixture = new Fixture();
    public GetOrderDetailsServiceTest()
    {
      _mockContext = new StuffKartContext();
      var _logger = new Mock<ILogger<GetOrderDetailsService>>();
      _orderDetailsService = new GetOrderDetailsService(_mockContext, _logger.Object);
    }
    [Fact]
    public async Task Given_Valid_SearchTerm_GetOrderDetails_Returns_WithExpectedResult()
    {
      //Arrange
      var result = await _orderDetailsService.SearchUserProduct("suresh@gmail.com");

      //Act
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(2, actualResult.Count);
    }
  }
}
