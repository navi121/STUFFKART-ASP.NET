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
  public class AddAdminUserControllerTest
  {
    private readonly Fixture _fixture = new Fixture();
    private readonly AddAdminUserController _controller;
    private readonly Mock<IAddAdminUserService> _addAdminUserService;
    public AddAdminUserControllerTest()
    {
      _addAdminUserService = new Mock<IAddAdminUserService>();
      var _logger = new Mock<ILogger<AddAdminUserController>>();
      _controller = new AddAdminUserController(_addAdminUserService.Object, _logger.Object);
    }

    [Fact]
    public async Task Given_Valid_Admin_UserDetails_Returns200OK()
    {
      //arrange
      var userDetailsRequest = _fixture.Create<UserDetails>();

      //act
      _addAdminUserService.Setup(x => x.AddAdminUser(userDetailsRequest)).ReturnsAsync(true);
      var result = await _controller.AddAdminUser(userDetailsRequest) as OkResult;

      //assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Given_InValid_Admin_UserDetails_Returns400BadRequest()
    {
      //Arrange
      var userDetailsRequest = _fixture.Create<UserDetails>();

      //Act
      _addAdminUserService.Setup(x => x.AddAdminUser(userDetailsRequest)).ReturnsAsync(false);
      var result = await _controller.AddAdminUser(userDetailsRequest) as BadRequestResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Given_InValid_Admin_UserDetails_Returns500InternalServer_Error()
    {
      //Arrange
      var userDetailsRequest = _fixture.Create<UserDetails>();
      var errorMessage = _fixture.Create<string>();

      //Act
      _addAdminUserService.Setup(x => x.AddAdminUser(userDetailsRequest)).ThrowsAsync(new Exception(errorMessage));
      var result = await _controller.AddAdminUser(userDetailsRequest) as ObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
