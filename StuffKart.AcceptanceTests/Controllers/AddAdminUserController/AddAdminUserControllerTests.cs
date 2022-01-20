using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using System.Threading.Tasks;
using AutoFixture;
using Xunit;
using Newtonsoft.Json;
using StuffKartProject.Models;
using Microsoft.AspNetCore.Http;

namespace StuffKartProject.AcceptanceTests.Controllers.AddAdminUserController
{
  [Collection(TestCollectionName)]
  public class AddAdminUserControllerTests : AddAdminUserControllerTestBase
  {
    private readonly AutoFixture.Fixture _fixture;
    public AddAdminUserControllerTests()
    {
      _fixture = new AutoFixture.Fixture();
    }

    [Fact]
    public async Task Add_Admin_Details_Returns_200OK()
    {
      var requestData = _fixture.Create<UserDetails>();
      requestData.UserId = 0;
      var request = PostAdminCredentials(requestData);
      var response = await HttpClient.SendAsync(request);

      response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GivenInvalidRequest_To_AddAdmin_User_Details_Returns_400BadRequest()
    {
      var requestData = _fixture.Build<UserDetails>().With(x => x.MobileNumber, 9080352867).Create();

      var request = PostAdminCredentials(requestData);
      var response = await HttpClient.SendAsync(request);

      response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task WhenRequestIsNull_AddAdmin_UserDetail_Returns_500Internal_ServerError()
    {
      var request = PostAdminCredentials();

      var response = await HttpClient.SendAsync(request);

      response.StatusCode.Should().Be((System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError);
    }
  }
}
