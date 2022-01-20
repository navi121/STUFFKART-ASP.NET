using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StuffKartProject.Constant;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Controllers
{
  [Route("ForgetPassword")]
  [ApiController]
  public class ForgetPasswordController : ControllerBase
  {
    private readonly IForgetPasswordService _checkUserService;
    private readonly ILogger _logger;
    public ForgetPasswordController(IForgetPasswordService checkUserService, ILogger<ForgetPasswordController> logger)
    {
      _checkUserService = checkUserService;
      _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> ForgetPassword(UserDetails userRequest)
    {
      try
      {
        if (string.IsNullOrEmpty(userRequest.Email))
        {
          return BadRequest();
        }

        var resetStatus = await _checkUserService.CheckUserEmail(userRequest);
        _logger.LogInformation("Getting User Reset Request Correctly");

        if (resetStatus == false)
        {
          _logger.LogWarning("Invalid Reset Request from User");

          return Unauthorized(Messages.InvalidDetails);
        }
        _logger.LogInformation("Succesfully verified User");

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
