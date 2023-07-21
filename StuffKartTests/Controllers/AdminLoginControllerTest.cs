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
  public class AdminLoginControllerTest
  {
    private readonly Mock<IAdminLoginService> _adminLoginService;
    private readonly AdminLoginController _controller;
    private readonly Fixture _fixture = new Fixture();

    public AdminLoginControllerTest()
    {
      _adminLoginService = new Mock<IAdminLoginService>();
      var _logger = new Mock<ILogger<AdminLoginController>>();
      _controller = new AdminLoginController(_adminLoginService.Object, _logger.Object);
    }
    [Fact]
    public void Given_Admin_ValidDetails_IsValid_Returns200OK()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();

      //act
      _adminLoginService.Setup(x => x.ValidateAdminUserAsync(request)).Returns("token");

      var result = _controller.AdminUserLoginAsync(request) as OkObjectResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public void Given_ValidDetails_IsInValid_Admin_Returns401_UnAuthorized()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();

      //act
      _adminLoginService.Setup(x => x.ValidateAdminUserAsync(request));

      var result = _controller.AdminUserLoginAsync(request) as UnauthorizedResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }

    [Fact]
    public void Given_ValidDetails_IsInValid_Admin_Returns400_badRequest()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();
      request.Email = "";

      //act
      _adminLoginService.Setup(x => x.ValidateAdminUserAsync(request));

      var result = _controller.AdminUserLoginAsync(request) as BadRequestResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Given_ValidDetails_Admin_IsInValid_Returns500_Internal_ServerError()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();
      var errorMessage = _fixture.Create<string>();

      //act
      _adminLoginService.Setup(x => x.ValidateAdminUserAsync(request)).Throws(new Exception(errorMessage));

      var result =  _controller.AdminUserLoginAsync(request) as ObjectResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
