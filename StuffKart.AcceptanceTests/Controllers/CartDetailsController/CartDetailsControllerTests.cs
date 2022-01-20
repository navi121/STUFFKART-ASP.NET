using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartProject.AcceptanceTests.Controllers.CartDetailsController
{
  public class CartDetailsControllerTests : CartDetailsControllerTestBase
  {
    private readonly AutoFixture.Fixture _fixture;

    public CartDetailsControllerTests()
    {
      _fixture = new AutoFixture.Fixture();
    }

    [Fact]
    public async Task CartDetails_Post_Methoed_Returns_200Ok()
    {
      var cartDetailRequest = _fixture.Create<CartDetail>();
      var userEmail = "testemail@gmail.com";

      var response = MakeRequestAsync(userEmail, cartDetailRequest);
      var result =await HttpClient.SendAsync(response);

      result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact]
    public async Task CartDetails_Post_Methoed_Returns_500InternalServer_Error()
    {
      var response = MakeRequestAsync();
      var result = await HttpClient.SendAsync(response);

      result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
  }
}
