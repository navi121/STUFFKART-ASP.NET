using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class AddUserDetailsService : IAddUserDetailsService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;

    public AddUserDetailsService(StuffKartContext context, ILogger<AddUserDetailsService> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<bool> AddUser(UserDetails userDetail)
    {
      _context.UserDetails.Add(userDetail);
      _logger.LogInformation("Getting userDetails from User : Line Number 23");

      if (UserDetailExists(userDetail.MobileNumber))
      {
        _logger.LogError("Given Mobile Number is InValid and Returning false : Line Number 28");

        return false;        
      }

      await _context.SaveChangesAsync();
      _logger.LogInformation("Saved user given details Successfully : Line Number 32");

      return true;
    }

    public bool UserDetailExists(long id)
    {
      _logger.LogInformation("Checking Mobile Number is Same or Equal");

      return _context.UserDetails.Any(e => e.MobileNumber == id);
    }
  }
}

