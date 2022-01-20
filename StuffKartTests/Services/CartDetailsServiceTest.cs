using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class CartDetailsServiceTest
  {
    private readonly StuffKartContext _mockContext;
    private readonly CartDetailsService _cartDetailsService;
    private readonly Fixture _fixture = new Fixture();
    public CartDetailsServiceTest()
    {
      _mockContext = new StuffKartContext();
      var _logger = new Mock<ILogger<CartDetailsService>>();
      _cartDetailsService = new CartDetailsService(_mockContext, _logger.Object);
    }

    [Fact]
    public async Task Given_Valid_Cart_Details_To_Service_Returns_true()
    {
      //Arrange
      var cartDetailRequest = _fixture.Create<CartDetail>();
      var userEmail = "naveenchpt@gmail.com";

      //Act
      var result = await _cartDetailsService.AddCartDetails(userEmail,cartDetailRequest);

      //Assert
      Assert.True(result);
    }
  }
}
