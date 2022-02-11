using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace StuffKartProject.Controllers
{
  [Route("UserUpdate")]
  [ApiController]
  public class UserDetailsController : ControllerBase
  {
    private readonly IUserDetailService _userDetailService;
    private readonly ILogger _logger;
    public UserDetailsController(IUserDetailService userDetailService, ILogger<UserDetailsController> logger)
    {
      _userDetailService = userDetailService;
      _logger = logger;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UserDetailsUpdate(UserDetails updateRequest)
    {
      try
      {
        var updateStatus = await _userDetailService.UpdateUser(updateRequest);

        if (updateStatus == false)
        {
          return BadRequest();
        }

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
