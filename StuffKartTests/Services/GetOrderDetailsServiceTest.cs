using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class GetOrderDetailsServiceTest
  {
    private readonly StuffKartContext context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<GetOrderDetailsService>> _logger;
    public GetOrderDetailsServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<GetOrderDetailsService>>();
    }

    [Fact]
    public async Task Given_Valid_SearchTerm_GetOrderDetails_Returns_WithExpectedResult()
    {
      //Arrange
      var userRequest = randomUserDetail();
      var orderRequest = fixtureOrderDetails(userRequest);
      context.Orders.Add(orderRequest);
      context.UserDetails.Add(userRequest);
      await context.SaveChangesAsync();
      var service = new GetOrderDetailsService(context, _logger.Object);

      //Act
      var result = await service.SearchUserProduct(userRequest.Email);
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(context.Orders, actualResult);
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
