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
  public class GetCartDetailsServiceTest
  {
    private readonly StuffKartContext context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<GetCartDetailsService>> _logger;
    public GetCartDetailsServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<GetCartDetailsService>>();
    }

    [Fact]
    public async Task Given_Valid_Cart_Details_To_Service_Returns_true()
    {
      //Arrange
      var userRequest = randomUserDetail();
      var cartRequest = randomCartDetail(userRequest);
      context.CartDetails.Add(cartRequest);
      context.UserDetails.Add(userRequest);
      await context.SaveChangesAsync();
      var service = new GetCartDetailsService(context, _logger.Object);

      //Act
      var result = await service.SearchUserProduct(userRequest.Email);
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(context.CartDetails,actualResult);
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
