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
  public class UserDetailsControllerTest
  {
    private readonly Mock<IUserDetailService> _userDetailService;
    private readonly UserDetailsController _controller;
    private readonly Fixture _fixture = new Fixture();
    public UserDetailsControllerTest()
    {
      _userDetailService = new Mock<IUserDetailService>();
      var _logger = new Mock<ILogger<UserDetailsController>>();
      _controller = new UserDetailsController(_userDetailService.Object, _logger.Object);
    }

    [Fact]
    public async Task Get_All_The_ValuesIn_UserDetails()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();

      //act
      _userDetailService.Setup(x => x.UpdateUser(request)).ReturnsAsync(true);
      var result = await _controller.UserDetailsUpdate(request) as OkResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Delete_Valid_UserDetail_Will_Returns200OK()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();

      //act
      _userDetailService.Setup(x => x.UpdateUser(request)).ReturnsAsync(false);
      var result = await _controller.UserDetailsUpdate(request) as BadRequestResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Delete_InValid_UserDetail_Will_Returns404NotFound()
    {
      //arrange
      var request = _fixture.Create<UserDetails>();
      var errorMessage = _fixture.Create<string>();

      //act
      _userDetailService.Setup(x => x.UpdateUser(request)).ThrowsAsync(new Exception(errorMessage));
      var result = await _controller.UserDetailsUpdate(request) as ObjectResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
