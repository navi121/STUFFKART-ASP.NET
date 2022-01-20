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
  [ApiController]
  public class UserLoginController : ControllerBase
  {
    private readonly IUserLoginService _userLoginService;
    private readonly ILogger _logger;
    public UserLoginController(IUserLoginService userLoginService, ILogger<UserLoginController> logger)
    {
      _userLoginService = userLoginService;
      _logger = logger;
    }
    
    [Route("UserLogin")]
    [HttpPost]
    public IActionResult UserLoginAsync(UserDetails loginRequest)
    {
      try
      {
        _logger.LogInformation("Receving Login Request");
        if(string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
        {
          return BadRequest();
        }

        var loginStatus = _userLoginService.ValidateUserAsync(loginRequest);
        
        if (loginStatus == null)
        {
          _logger.LogWarning("Received InValid login Request");

          return Unauthorized();
        }
        _logger.LogInformation("User Login Succesfull");

        return Ok(loginStatus);
      }
      catch (Exception ex)
      {
        _logger.LogError("Received Exception Error while running");

        return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
      }
    }
  }
}
