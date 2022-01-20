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
  public class AddUserDetailsControllerTest
  {
    private readonly Fixture _fixture = new Fixture();
    private readonly AddUserDetailsController _controller;
    private readonly Mock<IAddUserDetailsService> _addUserDetailService;
    //private readonly ILogger _logger;
    public AddUserDetailsControllerTest() 
    {
      _addUserDetailService = new Mock<IAddUserDetailsService>();
      var _logger = new Mock<ILogger<AddUserDetailsController>>();
      _controller = new AddUserDetailsController(_addUserDetailService.Object,_logger.Object);
    }

    [Fact]
    public async Task Given_Valid_UserDetails_Returns200OK()
    {
      //Arrange
      var userDetailsRequest = _fixture.Create<UserDetails>();
      _addUserDetailService.Setup(x => x.AddUser(userDetailsRequest)).ReturnsAsync(true);

      //Act
      var result =await _controller.AddUserDetail(userDetailsRequest) as OkResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Given_InValid_UserDetails_Returns400BadRequest()
    {
      //Arrange
      var userDetailsRequest = _fixture.Create<UserDetails>();
      _addUserDetailService.Setup(x => x.AddUser(userDetailsRequest)).ReturnsAsync(false);

      //Act
      var result =await _controller.AddUserDetail(userDetailsRequest) as BadRequestResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

   
    [Fact]
    public async Task Given_InValid_UserDetails_Returns500InternalServer_Error()
    {
      //Arrange
      var userDetailsRequest = _fixture.Create<UserDetails>();
      var errorMessage = _fixture.Create<string>();

      //Act
      _addUserDetailService.Setup(x => x.AddUser(userDetailsRequest)).ThrowsAsync(new Exception(errorMessage));
      var result = await _controller.AddUserDetail(userDetailsRequest) as ObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
