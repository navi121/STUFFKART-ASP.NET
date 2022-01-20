using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    private readonly GetDashBoardProductsController _controller;
    private readonly Fixture _fixture;
    public GetDashBoardProductsControllerTest()
    {
      _fixture = new Fixture();
      _context = new StuffKartContext();
      var _logger = new Mock<ILogger<GetDashBoardProductsController>>();
      _controller = new GetDashBoardProductsController(_context, _logger.Object);
    }
    [Fact]
    public async Task GET_retrieves_DashBoard_Products()
    {
      //Act
      var result = _controller.GetProductDetails() as OkObjectResult;
      var expectedResult = ((IEnumerable<UploadProducts>)result.Value).ToList();

      //Assert
      Assert.Equal(_context.Products, expectedResult);
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
  }
}
