using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StuffKartProject.Constant;
using StuffKartProject.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using StuffKartProject.Services.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace StuffKartProject.Controllers
{

  [ApiController]
  public class AddUserDetailsController : ControllerBase
  {
    private readonly IAddUserDetailsService _addUserDetailsService;
    private readonly ILogger _logger;

    public AddUserDetailsController(IAddUserDetailsService addUserDetailsService, ILogger<AddUserDetailsController> logger)
    {
      _addUserDetailsService = addUserDetailsService;
      _logger = logger;
    }

    [Route("AddUser")]
    [HttpPost]
    public async Task<IActionResult> AddUserDetail(UserDetails userDetail)
    {
      try
      {
        var requestStatus = await _addUserDetailsService.AddUser(userDetail);
        _logger.LogInformation("Getting Adding User Details in Table");

        if (requestStatus == false)
        {
          _logger.LogError("Getting Invalid User Details");

          return BadRequest();
        }
        _logger.LogInformation("Successfully Saved User Detail");

        return Ok();
      }
      catch (Exception ex)
      {
        _logger.LogWarning("Getting Exception Error");

        return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
      }
    }
  }
}
