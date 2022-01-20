using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StuffKartProject.Constant;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Controllers
{

  [ApiController]
  public class OrderDetailsController : ControllerBase
  {
    private readonly IOrderDetailsService _orderDetailsService;
    private readonly ILogger _logger;

    public OrderDetailsController(IOrderDetailsService orderDetailsService, ILogger<OrderDetailsController> logger)
    {
      _orderDetailsService = orderDetailsService;
      _logger = logger;
    }

    [HttpPost("PlaceOrder/{userId}")]
    public async Task<IActionResult> AddOrdersDetail(string userId, List<OrderDetails> orderDetails)
    {
      try
      {
        var result =await _orderDetailsService.PlaceOrder(userId,orderDetails);

        _logger.LogInformation("Successfully Saved User Detail");
        if (result == true)
        {

          return Ok();
        }
        else
          return BadRequest();
      }
      catch (Exception ex)
      {
        _logger.LogWarning("Getting Exception Error");

        return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
      }
    }
  }
}
