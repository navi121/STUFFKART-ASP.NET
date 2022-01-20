using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using System;
using System.Linq;

namespace StuffKartProject.Controllers
{

  [Route("GetProductsDetails")]
  [ApiController]
  public class GetDashBoardProductsController : ControllerBase
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;

    public GetDashBoardProductsController(StuffKartContext context, ILogger<GetDashBoardProductsController> logger)
    {
      _context = context;
      _logger = logger;
    }

    [HttpGet]
    public IActionResult GetProductDetails()
    {     
      _logger.LogInformation("Getting Products List");

      try
      {
        var getProductsDetails = _context.Products.ToList();

        if(getProductsDetails == null)
        {
          _logger.LogWarning("There is no Products in DB returning NoContent Error");

          return NoContent();
        }

        return Ok(getProductsDetails);
      }

      catch (Exception ex)
      {
        _logger.LogError("Getting exception Error while Getting Product Details");

        return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
      }
    }
  }
}
