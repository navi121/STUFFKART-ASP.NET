using AutoFixture;
using Microsoft.EntityFrameworkCore;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Controllers
{
  public class CategoryServiceTest
  {
    private readonly StuffKartContext context;
    private readonly Fixture _fixture = new();
    public CategoryServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      context = new StuffKartContext(dboptions.Options);
    }

    [Fact]
    public async Task Given_Valid_category_GetDashBoard_Returns_CategoryMen()
    {
      //Arrange
      var categories = new List<UploadProducts>() { fixtureCreate("men"), fixtureCreate("men") };
      context.Products.AddRange(categories);
      await context.SaveChangesAsync();
      var service = new CategoryService(context);

      //Act
      var result = await service.DivideCategory("men");
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(categories.Count, actualResult.Count);  
    }

    [Fact]
    public async Task Given_Valid_category_GetDashBoard_Returns_Category_WoMen()
    {
      //Arrange
      var categories = new List<UploadProducts>() { fixtureCreate("women"), fixtureCreate("women") };
      context.Products.AddRange(categories);
      await context.SaveChangesAsync();
      var service = new CategoryService(context);

      //Act
      var result = await service.DivideCategory("women");
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(categories.Count, actualResult.Count);
    }

    [Fact]
    public async Task Given_Valid_category_GetDashBoard_Returns_Category_Kids()
    {
      //Arrange
      var categories = new List<UploadProducts>() { fixtureCreate("kid"), fixtureCreate("kid") };
      context.Products.AddRange(categories);
      await context.SaveChangesAsync();
      var service = new CategoryService(context);

      //Act
      var result = await service.DivideCategory("kid");
      var actualResult = result.ToList();

      //Assert
      Assert.Equal(categories.Count, actualResult.Count);
    }

    private UploadProducts fixtureCreate(string CategoryName)
    {
      var detail = _fixture.Create<UploadProducts>();
      detail.Category = CategoryName;

      return detail;
    }
  }
}
