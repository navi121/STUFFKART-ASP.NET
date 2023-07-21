using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StuffKartProject.Services.Interfaces;

namespace StuffKartProject.Controllers
{
  [ApiController]
  public class GetUserDetailController : ControllerBase
  {
    private readonly IGetUserDetailService _getUserDetailsService;
    private readonly ILogger _logger;
    public GetUserDetailController(IGetUserDetailService getUserDetailsService, ILogger<GetUserDetailController> logger)
    {
      _getUserDetailsService = getUserDetailsService;
      _logger = logger;
    }

    [HttpGet("GetUserDetail/{userEmail}")]
    [Authorize]
    public async Task<IActionResult> getUserDetail(string userEmail)
    {
      try
      {
        var userDetail = await _getUserDetailsService.getUser(userEmail);

        if (userDetail.Count() == 0)
        {
          return BadRequest();
        }

        return Ok(userDetail);
      }

      catch(Exception ex)
      {
        _logger.LogError("Received Exception Error while running");

        return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
      }      
    }
  }
}
