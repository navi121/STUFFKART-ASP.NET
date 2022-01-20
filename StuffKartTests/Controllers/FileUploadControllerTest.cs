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
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Controllers
{
  public class FileUploadControllerTest
  {
    //private readonly FileUploadController _controller;
    //private readonly Mock<IFileUploadService> _fileUploadService;
    //private readonly Fixture _fixture = new Fixture();

    //public FileUploadControllerTest()
    //{
    //  _fileUploadService = new Mock<IFileUploadService>();
    //  var _logger = new Mock<ILogger<FileUploadController>>();
    //  _controller = new FileUploadController(_fileUploadService.Object, _logger.Object);
    //}

    //[Fact]
    //public async Task Given_Valid_File_Upload_Returns200Ok()
    //{
    //  //Arrange
    //  var uploadRequest = _fixture.Create<UploadProducts>();
    //  List<IFormFile> file = new List<FormFile>(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 0, "Data", "image.png");
    //  var ProductId = uploadRequest.ProductId;

    //  //Act
    //  _fileUploadService.Setup(x => x.ImageUpload(ProductId, file));
    //  var result = _controller.UploadImage(ProductId, file) as OkResult;

    //  //Assert
    //  result.StatusCode.Should().Be(StatusCodes.Status200OK);
    //}

    //[Fact]
    //public void Given_InValid_File_Returns500Internal_ServerError()
    //{
    //  //Arrange
    //  var uploadRequest = _fixture.Create<UploadProducts>();
    //  IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 0, "Data", "image.png");
    //  var ProductId = uploadRequest.ProductId;
    //  var errorMessage = _fixture.Create<string>();

    //  //Act
    //   _fileUploadService.Setup(x => x.ImageUpload(ProductId, file)).Throws(new Exception(errorMessage));
    //  var result =  _controller.UploadImage(ProductId, file) as ObjectResult;

    //  //Assert
    //  result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    //}
  }
}
