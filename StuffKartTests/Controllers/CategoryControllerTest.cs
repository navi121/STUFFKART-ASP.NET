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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Controllers
{
  public class CategoryControllerTest
  {
    private readonly Fixture _fixture = new Fixture();
    private readonly CategoryController _controller;
    private readonly Mock<ICategoryService> _categoryService;
    public CategoryControllerTest()
    {
      _categoryService = new Mock<ICategoryService>();
      var _logger = new Mock<ILogger<CategoryController>>();
      _controller = new CategoryController(_categoryService.Object, _logger.Object);
    }

    [Fact]
    public async Task Get_By_Category_Products_Returns200OK()
    {
      //Arrange
      var categoryTermRequest = "men";
      var expectedResult = _fixture.Build<UploadProducts>().With(x => x.ProductName, categoryTermRequest).CreateMany(2);

      //Act
      _categoryService.Setup(x => x.DivideCategory(categoryTermRequest)).ReturnsAsync(expectedResult);
      var result = await _controller.GetCategoryName(categoryTermRequest) as OkObjectResult;
      var actualResult = ((IEnumerable<UploadProducts>)result.Value).ToList();

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
      Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public async Task Given_categoryTermRequest_Returns_500InternalServer_Error()
    {
      //Arrange
      var categoryTermRequest = "men";
      var errorMessage = _fixture.Create<string>();

      //Act
      _categoryService.Setup(x => x.DivideCategory(categoryTermRequest)).ThrowsAsync(new Exception(errorMessage));
      var result = await _controller.GetCategoryName(categoryTermRequest) as ObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
