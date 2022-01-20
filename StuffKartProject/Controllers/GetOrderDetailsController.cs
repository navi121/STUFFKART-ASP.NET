using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
  public class GetOrderDetailsController : ControllerBase
  {
    private readonly IGetOrderDetailsService _getOrderDetailsService;
    private readonly ILogger _logger;
    public GetOrderDetailsController(IGetOrderDetailsService getOrderDetailsService, ILogger<GetOrderDetailsController> logger)
    {
      _getOrderDetailsService = getOrderDetailsService;
      _logger = logger;
    }

    [HttpGet("GetOrderDetail/{userEmail}")]
    [Authorize]
    public async Task<IActionResult> SearchProduct(string userEmail)
    {
      try
      {
        _logger.LogInformation("Receiving Product Name");

        var searchResult = await _getOrderDetailsService.SearchUserProduct(userEmail);
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
