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
  public class ForgetPasswordControllerTest
  {
    private readonly ForgetPasswordController _controller;
    private readonly Mock<IForgetPasswordService> _checkUser;
    private readonly Fixture _fixture = new Fixture();

    public ForgetPasswordControllerTest()
    {
      _checkUser = new Mock<IForgetPasswordService>();
      var _logger = new Mock<ILogger<ForgetPasswordController>>();
      _controller = new ForgetPasswordController(_checkUser.Object, _logger.Object);
    }

    [Fact]
    public async Task Given_Valid_Email_Login_Returns200Ok()
    {
      //Arrange
      var loginRequest = _fixture.Create<UserDetails>();

      //Act
      _checkUser.Setup(x => x.CheckUserEmail(loginRequest)).ReturnsAsync(true);
      var result = await _controller.ForgetPassword(loginRequest) as OkResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Given_InValid_Email_Login_Returns400BadRequest()
    {
      //Arrange
      var loginRequest = _fixture.Create<UserDetails>();

      //Act
      _checkUser.Setup(x => x.CheckUserEmail(loginRequest)).ReturnsAsync(false);
      var result = await _controller.ForgetPassword(loginRequest) as UnauthorizedObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }

    [Fact]
    public async Task Given_InValid_Email_Login_Returns500InternalServerError()
    {
      //Arrange
      var loginRequest = _fixture.Create<UserDetails>();
      var errorMessage = _fixture.Create<string>();

      //Act
      _checkUser.Setup(x => x.CheckUserEmail(loginRequest)).ThrowsAsync(new Exception(errorMessage));
      var result = await _controller.ForgetPassword(loginRequest) as ObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
