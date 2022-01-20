using System;
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
  public class CartDetailsController : ControllerBase
  {
    private readonly ICartDetailsService _cartDetailsService;
    private readonly ILogger _logger;

    public CartDetailsController(ICartDetailsService cartDetailsService, ILogger<CartDetailsController> logger)
    {
      _cartDetailsService = cartDetailsService;
      _logger = logger;
    }
    
    [HttpPost("AddCartDetails/{user}")]
    public async Task<IActionResult> AddCartDetail(string user,CartDetail cartDetail)
    {
      try
      {
        var requestResult = await _cartDetailsService.AddCartDetails(user,cartDetail);
        _logger.LogInformation("Getting CartDetails List");

        if (requestResult == false)
        {
          _logger.LogWarning("Received Invalid CartDetails");

          return BadRequest();
        }
        _logger.LogInformation("Susscesfully added CartDetails");

        return Ok();
      }
      catch (Exception ex)
      {
        _logger.LogError("Received Exception Error while running");

        return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
      }
    }

    [HttpPost("DeleteProducts/{user}")]
    public async Task<IActionResult> DeleteCartDetail(string user,CartDetail product)
    {
      try
      {
        var requestResult = await _cartDetailsService.DeleteCartDetails(user,product);
        _logger.LogInformation("Getting CartDetails List");

        if (requestResult == false)
        {
          _logger.LogWarning("Received Invalid CartDetails");

          return BadRequest();
        }
        _logger.LogInformation("Susscesfully added CartDetails");

        return Ok();
      }
      catch (Exception ex)
      {
        _logger.LogError("Received Exception Error while running");

        return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
      }
    }
  }
}
