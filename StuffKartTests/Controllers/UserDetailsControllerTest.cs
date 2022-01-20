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
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace StuffKartTests.Controllers
{
  public class UserDetailsControllerTest
  {
    private readonly Fixture _fixture = new Fixture();
    private readonly UserDetailsController _controller;
    private readonly StuffKartContext _context;
    public UserDetailsControllerTest()
    {
      _context = new StuffKartContext();
      _controller = new UserDetailsController(_context);
    }
    [Fact]
    public async Task Get_All_The_ValuesIn_UserDetails()
    {
      var result = _controller.GetUserDetails() as List<UserDetails>;
      Assert.Equal(_context.UserDetails, result);
    }
    [Fact]
    public async Task Delete_Valid_UserDetail_Will_Returns200OK()
    {
      var userRequest = 100;
      var result =await _controller.DeleteUserDetail(userRequest) as OkResult;
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Delete_InValid_UserDetail_Will_Returns404NotFound()
    {
      var userRequest = 4567;
      var result = await _controller.DeleteUserDetail(userRequest) as NotFoundResult;
      result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
  }
}
