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
  public class UserLoginControllerTest
  {
    private readonly Mock<IUserLoginService> _loginService;
    private readonly UserLoginController _controller;
    private readonly Fixture _fixture = new Fixture();

    public UserLoginControllerTest()
    {
      _loginService = new Mock<IUserLoginService>();
      var _logger = new Mock<ILogger<UserLoginController>>();
      _controller = new UserLoginController(_loginService.Object,_logger.Object);
    }

    [Fact]
    public void Given_ValidDetails_IsValid_Returns200OK()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();

      //act
      _loginService.Setup(x => x.ValidateUserAsync(request)).Returns("token");

      var result = _controller.UserLoginAsync(request) as OkObjectResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public void Given_ValidDetails_IsInValid_Returns401_UnAuthorized()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();

      //act
      _loginService.Setup(x => x.ValidateUserAsync(request));

      var result = _controller.UserLoginAsync(request) as UnauthorizedResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }

    [Fact]
    public void Given_ValidDetails_IsInValid_Returns400_BadRequest()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();
      request.Email = "";

      //act
      var result = _controller.UserLoginAsync(request) as BadRequestResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public void Given_ValidDetails_IsInValid_Returns500_Internal_ServerError()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();
      var errorMessage = _fixture.Create<string>();

      //act
      _loginService.Setup(x => x.ValidateUserAsync(request)).Throws(new Exception(errorMessage));

      var result = _controller.UserLoginAsync(request) as ObjectResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
