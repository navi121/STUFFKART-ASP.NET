using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StuffKartProject.Constant;
using StuffKartProject.Models;
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
    public async Task<IActionResult> getUserDetail(string userEmail)
    {
      var userDetail = await _getUserDetailsService.getUser(userEmail);

      if(userDetail == null)
      {
        return BadRequest();
      }

      return Ok(userDetail);
    }

  }
}
