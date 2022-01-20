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
  public class OrderDetailsServiceTest
  {
    private readonly StuffKartContext _mockContext;
    private readonly OrderDetailsService _orderDetailsService;
    private readonly Fixture _fixture = new Fixture();
    public OrderDetailsServiceTest()
    {
      _mockContext = new StuffKartContext();
      var _logger = new Mock<ILogger<OrderDetailsService>>();
      _orderDetailsService = new OrderDetailsService(_mockContext, _logger.Object);
    }

    [Fact]
    public async Task Given_Valid_Order_Details_To_Service_Returns_true()
    {
      //Arrange
      var cartDetailRequest = _fixture.Create<List<OrderDetails>>();
      var userEmail = "naveenchpt@gmail.com";

      //Act
      var result = await _orderDetailsService.PlaceOrder(userEmail, cartDetailRequest);

      //Assert
      Assert.True(result);
    }
  }
}
