using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoFixture;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace StuffKartProject.AcceptanceTests.Controllers.AddUserDetailController
{
  [Collection(TestCollectionName)]
  public class AddUserDetailControllerTests : AddUserDetailControllerTestBase
  {

    private readonly AutoFixture.Fixture _fixture;

    public AddUserDetailControllerTests()
    {
      _fixture = new AutoFixture.Fixture();
    }

    [Fact]
    public async Task Add_User_Details_Returns_200OK()
    {
      var requestData = _fixture.Create<UserDetails>();

      var request = PostUserCredentials(requestData);
      var response = await HttpClient.SendAsync(request);

      response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GivenInvalidRequest_To_Add_User_Details_Returns_400BadRequest()
    {
      var requestData = _fixture.Build<UserDetails>().With(x => x.MobileNumber, 9080352867).Create();

      var request = PostUserCredentials(requestData);
      var response = await HttpClient.SendAsync(request);

      response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task WhenRequestIsNull_Add_UserDetail_Returns_500Internal_ServerError()
    {
      var request = PostUserCredentials();

      var response = await HttpClient.SendAsync(request);

      response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }
  }
}
