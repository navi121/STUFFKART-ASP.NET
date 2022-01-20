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
  public class SearchBarControllerTest
  {
    private readonly SearchBarController _controller;
    private readonly Mock<ISearchBarService> _searchBarService;
    private readonly Fixture _fixture = new Fixture();

    public SearchBarControllerTest()
    {
      _searchBarService = new Mock<ISearchBarService>();
      var _logger = new Mock<ILogger<SearchBarController>>();
      _controller = new SearchBarController(_searchBarService.Object,_logger.Object);
    }

    [Fact]
    public async Task Given_Valid_SearchTerm_GetDashBoard_Returns200OK_WithExpectedResult()
    {
      //Arrange
      var searchTerm = "allen solly";
      var expectedResult = _fixture.Build<UploadProducts>().With(x => x.ProductName, searchTerm).CreateMany(2);

      //Act
      _searchBarService.Setup(x => x.SearchProduct(searchTerm)).ReturnsAsync(expectedResult);
      var result = await _controller.SearchProduct(searchTerm) as OkObjectResult;
      var actualResult = ((IEnumerable<UploadProducts>)result.Value).ToList();

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
      Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public async Task Given_InValid_SearchTerm_GetDashBoard_Returns200OK_WithAllProductResult()
    {
      //Arrange
      var givenTerm = "allen solly";
      var searchTerm = "puma";
      var expectedResult = _fixture.Build<UploadProducts>().With(x => x.ProductName, givenTerm).CreateMany(2);

      //Act
      _searchBarService.Setup(x => x.SearchProduct(searchTerm)).ReturnsAsync(expectedResult);
      var result = await _controller.SearchProduct(searchTerm) as OkObjectResult;
      var actualResult = ((IEnumerable<UploadProducts>)result.Value).ToList();

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status200OK);
      Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public async Task Given_InValid_SearchTerm_GetDashBoard_Returns500InternalServerError()
    {
      //Arrange
      var searchTerm = "allen solly";
      var errorMessage = _fixture.Create<string>();

      //Act
      _searchBarService.Setup(x => x.SearchProduct(searchTerm)).ThrowsAsync(new Exception(errorMessage));
      var result = await _controller.SearchProduct(searchTerm) as ObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
  }
}
