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
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Controllers
{
  public class OrderDetailsControllerTest
  {
    private readonly OrderDetailsController _controller;
    private readonly Mock<IOrderDetailsService> _orderDetailsService;
    private readonly Fixture _fixture = new Fixture();

    public OrderDetailsControllerTest()
    {
      _orderDetailsService = new Mock<IOrderDetailsService>();
      var _logger = new Mock<ILogger<OrderDetailsController>>();
      _controller = new OrderDetailsController(_orderDetailsService.Object, _logger.Object);
    }

    [Fact]
    public async Task Given_Valid_OrderDetails_Returns200Ok()
    {
      //Arrange
      var orderRequest = _fixture.Create<List<OrderDetails>>();
      var userId = "naveenchpt@gmail.com";

      //Act
      _orderDetailsService.Setup(x => x.PlaceOrder(userId, orderRequest)).ReturnsAsync(true);
      var result = await _controller.AddOrdersDetail(userId, orderRequest) as OkResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Given_Valid_OrderDetails_Returns_400BadRequest()
    {
      //Arrange
      var orderRequest = _fixture.Create<List<OrderDetails>>();
      var userId = "naveenchpt@gmail.com";

      //Act
      _orderDetailsService.Setup(x => x.PlaceOrder(userId, orderRequest)).ReturnsAsync(false);
      var result = await _controller.AddOrdersDetail(userId, orderRequest) as BadRequestResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Given_Valid_OrderDetails_Returns_500InternalServer_Error()
    {
      //Arrange
      var orderRequest = _fixture.Create<List<OrderDetails>>();
      var userId = "naveenchpt@gmail.com";
      var errorMessage = _fixture.Create<string>();

      //Act
      _orderDetailsService.Setup(x => x.PlaceOrder(userId, orderRequest)).ThrowsAsync(new Exception(errorMessage));
      var result = await _controller.AddOrdersDetail(userId, orderRequest) as ObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
