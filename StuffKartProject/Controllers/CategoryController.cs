using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Controllers
{
  [ApiController]
  public class CategoryController : ControllerBase
  {
    private readonly ICategoryService _categoryService;
    private readonly ILogger _logger;
    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
      _categoryService = categoryService;
      _logger = logger;
    }

    [HttpGet("DivideCategory/{categoryName}")]
    public async Task<IActionResult> GetCategoryName(string categoryName)
    {
      try
      {
        var searchResult = await _categoryService.DivideCategory(categoryName);
        _logger.LogInformation("Getting Products List");

        return Ok(searchResult);
      }
      catch (Exception ex)
      {
        _logger.LogError("Received Exception Error While Running the Code");

        return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
      }
    }
  }
}
