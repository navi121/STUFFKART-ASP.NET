using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace StuffKartProject.Controllers
{

  [ApiController]
  public class DeleteCartDetailController : ControllerBase
  {
    private readonly IDeleteCartDetailService _deleteCartDetailsService;
    private readonly ILogger _logger;

    public DeleteCartDetailController(IDeleteCartDetailService deleteCartDetailsService, ILogger<DeleteCartDetailController> logger)
    {
      _deleteCartDetailsService = deleteCartDetailsService;
      _logger = logger;
    }

    [HttpPost("DeleteProducts/{user}")]
    public async Task<IActionResult> DeleteCartDetail(string user, CartDetail product)
    {
      try
      {
        var requestResult = await _deleteCartDetailsService.DeleteCartDetails(user, product);
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
