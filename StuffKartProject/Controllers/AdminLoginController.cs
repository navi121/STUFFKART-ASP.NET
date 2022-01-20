using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Controllers
{
  [Route("AdminUserLogin")]
  [ApiController]
  public class AdminLoginController : ControllerBase
  {
    private readonly IAdminLoginService _userLoginService;
    private readonly ILogger _logger;
    public AdminLoginController(IAdminLoginService userLoginService, ILogger<AdminLoginController> logger)
    {
      _userLoginService = userLoginService;
      _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult AdminUserLoginAsync(UserDetails adminLoginRequest)
    {
      try
      {
        _logger.LogInformation("Receving Login Request");
        if (string.IsNullOrEmpty(adminLoginRequest.Email) || string.IsNullOrEmpty(adminLoginRequest.Password))
        {
          return BadRequest();
        }

        var token =  _userLoginService.ValidateAdminUserAsync(adminLoginRequest);
        if (token == null)
        {
          _logger.LogWarning("Received InValid login Request");

          return Unauthorized();
        }
        _logger.LogInformation("User Login Succesfull");

        return Ok(token);
      }
      catch (Exception ex)
      {
        _logger.LogError("Received Exception Error while running");

        return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
      }
    }
  }
}
