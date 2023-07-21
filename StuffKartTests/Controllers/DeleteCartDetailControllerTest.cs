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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Controllers
{
  public class DeleteCartDetailControllerTest
  {
    private readonly Fixture _fixture = new Fixture();
    private readonly DeleteCartDetailController _controller;
    private readonly Mock<IDeleteCartDetailService> _cartDetailService;
    string user = "naveenchpt@gmail.com";

    public DeleteCartDetailControllerTest()
    {
      _cartDetailService = new Mock<IDeleteCartDetailService>();
      var _logger = new Mock<ILogger<DeleteCartDetailController>>();
      _controller = new DeleteCartDetailController(_cartDetailService.Object, _logger.Object);
    }

    [Fact]
    public async Task Delete_CartDetail_Returns_200Ok()
    {
      //Arrange
      var cartDetail = _fixture.Create<CartDetail>();

      //Act
      _cartDetailService.Setup(x => x.DeleteCartDetails(user, cartDetail)).ReturnsAsync(true);
      var result = await _controller.DeleteCartDetail(user, cartDetail) as OkResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Delete_CartDetail_returns_400BadRequest()
    {
      //Arrange
      var cartDetail = _fixture.Create<CartDetail>();

      //Act
      _cartDetailService.Setup(x => x.DeleteCartDetails(user, cartDetail)).ReturnsAsync(false);
      var result = await _controller.DeleteCartDetail(user, cartDetail) as BadRequestResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Delete_CartDetail_Returns_500InternalServerError()
    {
      //Arrange
      var cartDetail = _fixture.Create<CartDetail>();
      var errorMessage = _fixture.Create<string>();

      //Act
      _cartDetailService.Setup(x => x.DeleteCartDetails(user, cartDetail)).ThrowsAsync(new Exception(errorMessage));
      var result = await _controller.DeleteCartDetail(user, cartDetail) as ObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
