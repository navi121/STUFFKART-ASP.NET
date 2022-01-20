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
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Controllers
{
  public class GetOrderDetailsControllerTest
  {
    private readonly GetOrderDetailsController _controller;
    private readonly Mock<IGetOrderDetailsService> _getOrderDetailsService;
    private readonly Fixture _fixture = new Fixture();

    public GetOrderDetailsControllerTest()
    {
      _getOrderDetailsService = new Mock<IGetOrderDetailsService>();
      var _logger = new Mock<ILogger<GetOrderDetailsController>>();
      _controller = new GetOrderDetailsController(_getOrderDetailsService.Object, _logger.Object);
    }
    [Fact]
    public async Task Get_CartDetail_Products_In_DB_for_Specified_User_as200OK()
    {
      //Arrange
      var userEmail = "naveenchpt@gmail.com";
      var expectedResult = _fixture.Build<OrderDetails>().With(x => x.UserId, 1).CreateMany(2);

      //Act
      _getOrderDetailsService.Setup(x => x.SearchUserProduct(userEmail)).ReturnsAsync(expectedResult);
      var result = await _controller.SearchProduct(userEmail) as OkObjectResult;
      var actualResult = ((IEnumerable<OrderDetails>)result.Value).ToList();

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
      Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public async Task Get_retrieves_CartDetail_Products_Returns500Internal_serverError()
    {
      //Arrange
      var userEmail = "naveenchpt@gmail.com";
      var errorMessage = _fixture.Create<string>();

      //Act
      _getOrderDetailsService.Setup(x => x.SearchUserProduct(userEmail)).ThrowsAsync(new Exception(errorMessage));
      var result = await _controller.SearchProduct(userEmail) as ObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
