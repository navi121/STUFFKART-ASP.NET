using AutoFixture;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Controllers
{
  public class CategoryServiceTest
  {
    private readonly CategoryService _service;
    private readonly Fixture _fixture = new Fixture();
    private readonly StuffKartContext _mockContext;
    public CategoryServiceTest()
    {
      _mockContext = new StuffKartContext();
    }
    [Fact]
    public async Task Given_Valid_category_GetDashBoard_Returns_CategoryMen()
    {
      //Arrange
      var _categoryService = new CategoryService(_mockContext);

      //Act
      var result = await _categoryService.DivideCategory("men");
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(4, actualResult.Count);  
    }

    [Fact]
    public async Task Given_Valid_category_GetDashBoard_Returns_Category_WoMen()
    {
      //Arrange
      var _categoryService = new CategoryService(_mockContext);

      //Act
      var result = await _categoryService.DivideCategory("women");
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(1, actualResult.Count);
    }

    [Fact]
    public async Task Given_Valid_category_GetDashBoard_Returns_Category_Kids()
    {
      //Arrange
      var _categoryService = new CategoryService(_mockContext);

      //Act
      var result = await _categoryService.DivideCategory("kid");
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(4, actualResult.Count);
    }
  }
}
