using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace StuffKartProject.Controllers
{
  [ApiController]
  public class FileUploadController : ControllerBase
  {
    private readonly IFileUploadService _imageUploadService;
    private readonly ILogger _logger;
    public FileUploadController(IFileUploadService imageUploadService, ILogger<FileUploadController> logger)
    {
      _imageUploadService = imageUploadService;
      _logger = logger;
    }

    [HttpPost("UploadImage/{id}")]
    public IActionResult UploadImage(int id, List<IFormFile> files)
    {
      _logger.LogInformation("Receiving Product ID and Image");
      try
      {
        _imageUploadService.ImageUpload(id, files);

        _logger.LogInformation("Successfully saved Image on DataBase");

        return Ok();
      }
      catch(Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
      }
    }
  }
}
