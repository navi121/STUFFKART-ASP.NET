using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
  public class GetUserDetailsControllerTest
  {
    private readonly GetUserDetailController _controller;
    private readonly Mock<IGetUserDetailService> _getUserDetailsService;
    private readonly Fixture _fixture = new Fixture();

    public GetUserDetailsControllerTest()
    {
      _getUserDetailsService = new Mock<IGetUserDetailService>();
      var _logger = new Mock<ILogger<GetUserDetailController>>();
      _controller = new GetUserDetailController(_getUserDetailsService.Object, _logger.Object);
    }

    [Fact]
    public async Task Get_UserDetail_Returns200OK_with_ObjectResult()
    {
      //arrange
      var userRequest = "naveenchpt@gmail.com";
      var expectedResult = _fixture.Build<UserDetails>().With(x => x.UserId, 1).CreateMany(2);

      //act
      _getUserDetailsService.Setup(x => x.getUser(userRequest)).ReturnsAsync(expectedResult);

      var result = await _controller.getUserDetail(userRequest) as OkObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Get_UserDetail_BadRequest400()
    {
      //arrange
      var userRequest = "naveenchpt@gmail.com";

      //act
      _getUserDetailsService.Setup(x => x.getUser(userRequest));
      var result = await _controller.getUserDetail(userRequest) as BadRequestResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Get_UserDetail_Returns_500InternalServer_Error()
    {
      //Arrange
      var request = _fixture.Create<UserDetails>();
      var errorMessage = _fixture.Create<string>();

      //Act
      _getUserDetailsService.Setup(x => x.getUser(request.Email)).ThrowsAsync(new Exception(errorMessage));
      var result = await _controller.getUserDetail(request.Email) as ObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
