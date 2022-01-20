using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class GetCartDetailsServiceTest
  {
    private readonly StuffKartContext _mockContext;
    private readonly GetCartDetailsService _getCartDetailsService;
    private readonly Fixture _fixture = new Fixture();
    public GetCartDetailsServiceTest()
    {
      _mockContext = new StuffKartContext();
      var _logger = new Mock<ILogger<GetCartDetailsService>>();
      _getCartDetailsService = new GetCartDetailsService(_mockContext, _logger.Object);
    }
    [Fact]
    public async Task Given_Valid_Cart_Details_To_Service_Returns_true()
    {
      //Arrange
      var userEmail = "naveenchpt@gmail.com";

      //Act
      var result = await _getCartDetailsService.SearchUserProduct(userEmail);
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(2,actualResult.Count);
    }

  }
}
