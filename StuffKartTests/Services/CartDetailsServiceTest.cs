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
  public class CartDetailsServiceTest
  {
    private readonly StuffKartContext context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<CartDetailsService>> _logger;
    public CartDetailsServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
         .UseInMemoryDatabase(Guid.NewGuid().ToString());
      context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<CartDetailsService>>();
    }

    [Fact]
    public async Task Given_Valid_Cart_Details_To_Service_Returns_true()
    {
      //Arrange
      var userRequest = randomUserDetail();
      var cartRequest = randomCartDetail(userRequest);
      context.UserDetails.Add(userRequest);
      await context.SaveChangesAsync();
      var service = new CartDetailsService(context, _logger.Object);

      //Act
      var result = await service.AddCartDetails(userRequest.Email,cartRequest);

      //Assert
      Assert.True(result);
    }

    private CartDetail randomCartDetail(UserDetails userDetails)
    {
      var cartDetail = _fixture.Create<CartDetail>();
      cartDetail.UserId = userDetails.UserId;
      return cartDetail;

    }
    private UserDetails randomUserDetail()
    {
      return _fixture.Create<UserDetails>();
    }
  }
}
