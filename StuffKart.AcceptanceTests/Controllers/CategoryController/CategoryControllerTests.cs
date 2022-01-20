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

namespace StuffKartProject.AcceptanceTests.Controllers.CategoryController
{
  [Collection(TestCollectionName)]
  public class CategoryControllerTests : CategoryControllerTestBase
  {
    private readonly AutoFixture.Fixture _fixture;

    public CategoryControllerTests()
    {
      _fixture = new AutoFixture.Fixture();
    }

    [Fact]
    public async Task GetBy_CatetoryName_Method_Return200Ok_with_Values()
    {
      var entities = _fixture.Build<UploadProducts>().With(x => x.Category, "Men").CreateMany(3);

      var expectedResponse = entities.First();
      var response = await MakeRequestAsync(expectedResponse);
      var result = await response.Content.ReadAsStringAsync();

      var resultObject = JsonConvert.DeserializeObject<UploadProducts>(result);

      response.StatusCode.Should().Be(HttpStatusCode.OK);
      resultObject.Should().BeEquivalentTo(expectedResponse);

    }

    [Fact]
    public async Task GetBy_CatetoryName_Method_Return500_InternalServer_Error()
    {
      var entities = _fixture.Build<UploadProducts>().With(x => x.Category, "Men").CreateMany(3);
      entities.First().Category = "";
      var expectedResponse = entities.First();

      var response = await MakeRequestAsync(expectedResponse);

      var result =await response.Content.ReadAsStringAsync();
      var resultObject = JsonConvert.DeserializeObject<UploadProducts>(result);

      response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
  }
}
