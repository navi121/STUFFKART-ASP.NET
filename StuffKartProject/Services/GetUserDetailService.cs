using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class GetUserDetailService : IGetUserDetailService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;

    public GetUserDetailService(StuffKartContext context, ILogger<GetUserDetailService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<IEnumerable<UserDetails>> getUser(string email)
    {
      var userDetail =await _context.UserDetails.Where(x => x.Email == email).ToListAsync();

      return userDetail;
    }
  }
}
