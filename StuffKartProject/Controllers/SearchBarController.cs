using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StuffKartProject.Constant;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;

namespace StuffKartProject.Controllers
{
  [ApiController]
  public class SearchBarController : ControllerBase
  {
    private readonly ISearchBarService _searchBarService;
    private readonly ILogger _logger;
    public SearchBarController(ISearchBarService searchBarService, ILogger<SearchBarController> logger)
    {
      _searchBarService = searchBarService;
      _logger = logger;

    }

    [HttpGet("SearchProduct/{productName}")]
    public async Task<IActionResult> SearchProduct(string productName)
    {
      try
      {
        _logger.LogInformation("Receiving Product Name");

        var searchResult = await _searchBarService.SearchProduct(productName);
        _logger.LogInformation("Returning Product Details");

        return Ok(searchResult);
      }
      catch (Exception ex)
      {
        _logger.LogError("Received Exception Error while running");

        return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
      }
    }
  }
}
