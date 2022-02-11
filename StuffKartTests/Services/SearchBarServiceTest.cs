using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests
{
  public class SearchBarServiceTest
  {
    private readonly StuffKartContext _context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<SearchBarService>> _logger;

    public SearchBarServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      _context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<SearchBarService>>();
    }

    [Fact]
    public async Task Given_Valid_SearchTerm_GetDashBoard_Returns_WithExpectedResult()
    {
      //Arrange
      var categories = fixtureCreate();
      _context.Products.AddRange(categories);
      await _context.SaveChangesAsync();
      var service = new SearchBarService(_context, _logger.Object);

      //Act
      var result = await service.SearchProduct(categories.ProductName);
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(_context.Products,actualResult);
    }
    [Fact]
    public async Task Given_InValid_SearchTerm_GetDashBoard_Returns_WithAllProductResult()


{
      //Arrange
      var categories = new List<UploadProducts>() { fixtureCreate(), fixtureCreate() };
      _context.Products.AddRange(categories);
      await _context.SaveChangesAsync();
      var service = new SearchBarService(_context,_logger.Object);

      //Act
      var result = await service.SearchProduct("Duplicate Product");
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(_context.Products, actualResult);
    }

    private UploadProducts fixtureCreate()
    {
      var detail = _fixture.Create<UploadProducts>();

      return detail;
    }
  }
}
