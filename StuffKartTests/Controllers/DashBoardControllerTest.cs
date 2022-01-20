using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Controllers;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Controllers
{
  public class DashBoardControllerTest
  {
    private readonly DashBoardController _controller;
    private readonly Mock<IDashBoardService> _dashBoardService;
    private readonly Fixture _fixture = new Fixture();

    public DashBoardControllerTest()
    {
      _dashBoardService = new Mock<IDashBoardService>();
      var _logger = new Mock<ILogger<DashBoardController>>();
      _controller = new DashBoardController(_dashBoardService.Object, _logger.Object);
    }

    [Fact]
    public async Task Add_Valid_DashBoard_Products_Returns200OK()
    {
      //Arrange
      var dashBoardRequest = _fixture.Create<UploadProducts>();
      var productId = dashBoardRequest.ProductId;

      //Act
      _dashBoardService.Setup(x => x.AddProductDetailService(dashBoardRequest)).ReturnsAsync(productId);
      var result = _controller.AddProductDetail(dashBoardRequest);

      //Assert
      Assert.Equal(dashBoardRequest.ProductId, result.Result);
    }

    [Fact]
    public async Task Add_InValid_DashBoard_Products_Returns_500InternalServer_Error()
    {
      //Arrange
      var dashBoardRequest = _fixture.Create<UploadProducts>();
      var productId = dashBoardRequest.ProductId;
      var errorMessage = _fixture.Create<string>();

      //Act
      _dashBoardService.Setup(x => x.AddProductDetailService(dashBoardRequest)).Throws(new Exception(errorMessage));
      var result = _controller.AddProductDetail(dashBoardRequest);

      //Assert
      Assert.Equal(0,result.Result);
    }
  }
}
