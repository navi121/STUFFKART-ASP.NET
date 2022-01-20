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
  public class GetUserDetailsControllerTest
  {

    [Fact]
    public async Task Get_UserDetail_Returns200OK_with_ObjectResult()
    {
      var userRequest = 9080352867;
      var addUserDetail = new StuffKartContext();
      var expectedUser= addUserDetail.UserDetails.FindAsync(userRequest);
      var controller = new GetUserDetailsController(addUserDetail);
      var result = controller.GetUserDetail(userRequest) as OkObjectResult;
      Assert.Equal(expectedUser.Result, result.Value);
    }

    [Fact]
    public async Task Get_UserDetail_BadRequest400()
    {
      var userRequest = 12345;
      var addUserDetail = new StuffKartContext();
      var controller = new GetUserDetailsController(addUserDetail);
      var result = controller.GetUserDetail(userRequest) as NotFoundResult;
      result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
  }
}
