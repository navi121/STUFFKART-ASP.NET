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
  public class ResetPasswordControllerTest
  {
    private readonly ResetPasswordController _controller;
    private readonly Mock<IResetPasswordService> _resetPasswordService;
    private readonly Fixture _fixture = new Fixture();

    public ResetPasswordControllerTest()
    {
      _resetPasswordService = new Mock<IResetPasswordService>();
      var _logger = new Mock<ILogger<ResetPasswordController>>();
      _controller = new ResetPasswordController(_resetPasswordService.Object,_logger.Object);
    }

    [Fact]
    public async Task Given_Valid_Credentials_to_ResetPassword_Returns200Ok()
    {
      //Arrange
      var loginRequest = _fixture.Create<ResetPassword>();
      var MobileNumber = loginRequest.MobileNumber;

      //Act
      _resetPasswordService.Setup(x => x.ValidateUserAsync(MobileNumber, loginRequest)).ReturnsAsync(true);
      var result = await _controller.ResetPassword(MobileNumber, loginRequest) as OkResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Given_InValid_Credentials_ResetPassword_Returns400_Badrequest()
    {
      //Arrange
      var loginRequest = _fixture.Create<ResetPassword>();
      var MobileNumber = loginRequest.MobileNumber;

      //Act
      _resetPasswordService.Setup(x => x.ValidateUserAsync(MobileNumber, loginRequest)).ReturnsAsync(false);
      var result = await _controller.ResetPassword(MobileNumber, loginRequest) as BadRequestResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Given_InValid_Credentials_ResetPassword_Returns500InternalServerError()
    {
      //Arrange
      var loginRequest = _fixture.Create<ResetPassword>();
      var errorMessage = _fixture.Create<string>();
      var MobileNumber = loginRequest.MobileNumber;

      //Act
      _resetPasswordService.Setup(x => x.ValidateUserAsync(MobileNumber, loginRequest)).ThrowsAsync(new Exception(errorMessage));
      var result = await _controller.ResetPassword(MobileNumber, loginRequest) as ObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
