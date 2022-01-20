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
  [ApiController]
  public class ResetPasswordController : ControllerBase
  {
    private readonly IResetPasswordService _resetPasswordService;
    private readonly ILogger _logger;
    public ResetPasswordController(IResetPasswordService resetPasswordService, ILogger<ResetPasswordController> logger)
    {
      _resetPasswordService = resetPasswordService;
      _logger = logger;
    }

    [HttpPut("UserResetPassword/{MobileNumber}")]
    public async Task<IActionResult> ResetPassword([FromRoute] long MobileNumber, [FromBody] ResetPassword request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      try
      {
        _logger.LogInformation("Receiving User Request");

        var resetStatus = await _resetPasswordService.ValidateUserAsync(MobileNumber,request);
        if (resetStatus == false)
        {
          _logger.LogWarning("Received Request for Reset Password is Worng");

          return Unauthorized();
        }
        _logger.LogInformation("Received Request for reset password is succesfull");

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

