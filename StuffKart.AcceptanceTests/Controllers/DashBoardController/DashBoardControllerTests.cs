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

namespace StuffKartProject.AcceptanceTests.Controllers.DashBoardController
{
  [Collection(TestCollectionName)]
  public class DashBoardControllerTests : DashBoardControllerTestBase
  {
    private readonly AutoFixture.Fixture _fixture;

    public DashBoardControllerTests()
    {
      _fixture = new AutoFixture.Fixture();
    }

    [Fact]
    public async Task DashBoard_Controller_Returns_ProductID()
    {
      var products = _fixture.Create<UploadProducts>();

      var request = PostProductDetails(products);
      var response = await HttpClient.SendAsync(request);

      response.Should().Be(products.ProductId);
    }

    [Fact]
    public async Task DashBoard_Controller_Returns_0_AsProductId()
    {
      var request = PostProductDetails();
      var response = await HttpClient.SendAsync(request);

      response.Should().Be(0);
    }
  }
}
