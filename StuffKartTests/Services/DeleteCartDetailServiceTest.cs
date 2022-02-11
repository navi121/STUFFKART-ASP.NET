using AutoFixture;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class DeleteCartDetailServiceTest
  {
    private readonly StuffKartContext context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<DeleteCartDetailService>> _logger ;
    public DeleteCartDetailServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<DeleteCartDetailService>>();
    }

    [Fact]
    public async Task DeletecartDetailService_returns_True()
    {
      //Arrange
      var userRequest = randomUserDetail();
      var cartRequest = randomCartDetail(userRequest);      
      context.CartDetails.Add(cartRequest);
      context.UserDetails.Add(userRequest);
      await context.SaveChangesAsync();
      var service = new DeleteCartDetailService(context, _logger.Object);

      //Act
      var result = await service.DeleteCartDetails(userRequest.Email, cartRequest);

      //Assert
      Assert.True(result);
    }

    [Fact]
    public async Task DeletecartDetailService_returns_False()
    {
      //Arrange
      var userRequest = randomUserDetail();
      var cartRequest = randomCartDetail(userRequest);
      context.CartDetails.Add(cartRequest);
      context.UserDetails.Add(userRequest);
      await context.SaveChangesAsync();
      var service = new DeleteCartDetailService(context, _logger.Object);
      cartRequest.productName = "levis";

      //Act
      var result = await service.DeleteCartDetails(userRequest.Email, cartRequest);

      //Assert
      Assert.False(result);
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
