using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Controllers;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Controllers
{
  public class CartDetailsControllerTest
  {
    private readonly Fixture _fixture = new Fixture();
    private readonly CartDetailsController _controller;
    private readonly Mock<ICartDetailsService> _cartDetailService;
    string user = "naveenchpt@gmail.com";
    public CartDetailsControllerTest()
    {
      _cartDetailService = new Mock<ICartDetailsService>();
      var _logger = new Mock<ILogger<CartDetailsController>>();
      _controller = new CartDetailsController(_cartDetailService.Object, _logger.Object);
    }

    [Fact]
    public async Task Add_CartDetails_Products_Returns200OK()
    {
      //Arrange
      var cartDetail = _fixture.Create<CartDetail>();

      //Act
      _cartDetailService.Setup(x => x.AddCartDetails(user,cartDetail)).ReturnsAsync(true);
      var result = await _controller.AddCartDetail(user, cartDetail) as OkResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Add_InValid_cartDetails_Returns400BadRequest()
    {
      //Arrange
      var cartDetailsRequest = _fixture.Create<CartDetail>();

      //Act
      _cartDetailService.Setup(x => x.AddCartDetails(user,cartDetailsRequest)).ReturnsAsync(false);
      var result = await _controller.AddCartDetail(user, cartDetailsRequest) as BadRequestResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Add_InValid_cartDetails_Returns500InternalServer_Error()
    {
      //Arrange
      var cartDetailsRequest = _fixture.Create<CartDetail>();
      var errorMessage = _fixture.Create<string>();

      //Act
      _cartDetailService.Setup(x => x.AddCartDetails(user, cartDetailsRequest)).ThrowsAsync(new Exception(errorMessage));
      var result = await _controller.AddCartDetail(user, cartDetailsRequest) as ObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
