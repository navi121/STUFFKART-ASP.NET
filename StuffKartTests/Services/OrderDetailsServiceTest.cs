using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class OrderDetailsServiceTest
  {
    private readonly StuffKartContext context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<OrderDetailsService>> _logger;
    public OrderDetailsServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<OrderDetailsService>>();
    }

    [Fact]
    public async Task Given_Valid_Order_Details_To_Service_Returns_true()
    {
      //Arrange
      var userRequest = randomUserDetail();
      var cartDetailRequest = new List<OrderDetails>() { fixtureOrderDetails(userRequest),fixtureOrderDetails(userRequest)};
      context.UserDetails.Add(userRequest);
      await context.SaveChangesAsync();
      var service = new OrderDetailsService(context,_logger.Object);

      //Act
      var result = await service.PlaceOrder(userRequest.Email, cartDetailRequest);

      //Assert
      Assert.True(result);
    }
    private UserDetails randomUserDetail()
    {
      return _fixture.Create<UserDetails>();
    }
    private OrderDetails fixtureOrderDetails(UserDetails userDetails)
    {
      var orderDetail = _fixture.Create<OrderDetails>();
      orderDetail.UserId = userDetails.UserId;

      return orderDetail;
    }
  }
}
