using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Controllers;
using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests
{
  public class GetDashBoardProductsControllerTest
  {
    private readonly StuffKartContext _context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<GetDashBoardProductsController>> _logger;
    public GetDashBoardProductsControllerTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      _context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<GetDashBoardProductsController>>();
    }

    [Fact]
    public async Task GET_retrieves_DashBoard_Products()
    {
      //Arrange
      var products = new List<UploadProducts>() { fixtureProducts(),fixtureProducts()};
      _context.Products.AddRange(products);
      await _context.SaveChangesAsync();
      var controller = new GetDashBoardProductsController(_context,_logger.Object);

      //Act
      var result = controller.GetProductDetails() as OkObjectResult;
      var expectedResult = ((IEnumerable<UploadProducts>)result.Value).ToList();

      //Assert
      Assert.Equal(_context.Products, expectedResult);
    }

    [Fact]
    public async Task GET_retrieves_DashBoard_Products_Returns_NoContent()
    {
      //Arrange
      var controller = new GetDashBoardProductsController(_context, _logger.Object);

      //Act
      var result = controller.GetProductDetails() as NoContentResult;;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }

    [Fact]
    public void Get_DashBoard_Products_Returns500Internal_serverError()
    {
      //Arrange      
      var context = new Mock<StuffKartContext>();
      var _logger = new Mock<ILogger<GetDashBoardProductsController>>();
      var controller = new GetDashBoardProductsController(context.Object, _logger.Object);

      //Act
      context.Setup(x => x.Products).Throws(new Exception(""));
      controller.GetProductDetails();
      var result = controller.GetProductDetails() as ObjectResult;

      //Assert
      result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }

    private UploadProducts fixtureProducts()
    {
      return _fixture.Create<UploadProducts>();
    }
  }
}
