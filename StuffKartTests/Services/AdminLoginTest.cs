using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using StuffKartProject.Controllers;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests 
{
 public class AdminLoginTest
  {
  private readonly UserLoginController _controller;
  private readonly Mock<IUserLoginService> _loginService;
  private readonly Fixture _fixture = new Fixture();

  public AdminLoginTest()
  {
    _loginService = new Mock<IUserLoginService>();
    _controller = new UserLoginController(_loginService.Object);
  }

  [Fact]
  public async Task Given_Valid_Credentials_Login_Returns200Ok()
  {
    var loginRequest = _fixture.Create<UserDetails>();
    _loginService.Setup(x => x.ValidateAdminUserAsync(loginRequest)).ReturnsAsync(true);
    var result = await _controller.AdminLogin(loginRequest) as OkResult;
    result.StatusCode.Should().Be(StatusCodes.Status200OK);
  }

  [Fact]
  public async Task Given_InValid_Credentials()
  {
    var loginRequest = _fixture.Create<UserDetails>();
    _loginService.Setup(x => x.ValidateAdminUserAsync(loginRequest)).ReturnsAsync(false);
    var result = await _controller.AdminLogin(loginRequest) as BadRequestResult;
    result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
  }
}
}
