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
  public class GetCartDetailsController : ControllerBase
  {
    private readonly IGetCartDetailsService _getCartDetailsService;
    private readonly ILogger _logger;
    public GetCartDetailsController(IGetCartDetailsService getCartDetailsService, ILogger<GetCartDetailsController> logger)
    {
      _getCartDetailsService = getCartDetailsService;
      _logger = logger;
    }

    [HttpGet("GetCartDetails/{userEmail}")]
    [Authorize]
    public async Task<IActionResult> GetCartDetails(string userEmail)
    {
      _logger.LogInformation("Getting Cart Details");

      try
      {
        var cartProducts = await _getCartDetailsService.SearchUserProduct(userEmail);

        return Ok(cartProducts);
      }

      catch (Exception ex)
      {

        return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
      }
    }
  }
}
