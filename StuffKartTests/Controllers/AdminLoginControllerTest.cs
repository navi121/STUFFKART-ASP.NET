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
    public async Task Given_Admin_ValidDetails_IsValid_Returns200OK()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();

      //act
      _adminLoginService.Setup(x => x.ValidateAdminUserAsync(request)).ReturnsAsync(true);

      var result = await _controller.AdminUserLoginAsync(request) as OkResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Given_ValidDetails_IsInValid_Admin_Returns400_BadRequest()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();

      //act
      _adminLoginService.Setup(x => x.ValidateAdminUserAsync(request)).ReturnsAsync(false);

      var result = await _controller.AdminUserLoginAsync(request) as UnauthorizedResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }

    [Fact]
    public async Task Given_ValidDetails_Admin_IsInValid_Returns500_Internal_ServerError()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();
      var errorMessage = _fixture.Create<string>();

      //act
      _adminLoginService.Setup(x => x.ValidateAdminUserAsync(request)).ThrowsAsync(new Exception(errorMessage));

      var result = await _controller.AdminUserLoginAsync(request) as ObjectResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
