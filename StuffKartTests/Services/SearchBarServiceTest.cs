using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests
{
  public class SearchBarServiceTest
  {
    private readonly StuffKartContext _mockContext;
    private readonly SearchBarService _searchBarService;
    private readonly Fixture _fixture = new Fixture();
    public SearchBarServiceTest()
    {
      _mockContext = new StuffKartContext();
      var _logger = new Mock<ILogger<SearchBarService>>();
      _searchBarService = new SearchBarService(_mockContext, _logger.Object);
    }
    [Fact]
    public async Task Given_Valid_SearchTerm_GetDashBoard_Returns_WithExpectedResult()
    {
      //Arrange
      var result = await _searchBarService.SearchProduct("allen solly");

      //Act
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(1,actualResult.Count);
    }
    [Fact]
    public async Task Given_InValid_SearchTerm_GetDashBoard_Returns_WithAllProductResult()
    {
      //Arrange
      var result = await _searchBarService.SearchProduct("note");

      //Act
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(_mockContext.Products, actualResult);
    }
  }
}
