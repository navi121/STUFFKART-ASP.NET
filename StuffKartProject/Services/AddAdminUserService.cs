using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class AddAdminUserService : IAddAdminUserService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;

    public AddAdminUserService(StuffKartContext context, ILogger<AddAdminUserService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<bool> AddAdminUser(UserDetails userDetail)
    {
      userDetail.isAdmin = 1;
      _context.UserDetails.Add(userDetail);
      _logger.LogInformation("Getting userDetails from User");

      if (UserDetailExists(userDetail.MobileNumber))
      {
        _logger.LogError("Given Mobile Number is InValid and Returning false : Line Number 44");

        return false;
      }
      await _context.SaveChangesAsync();

      return true;
    }
    public bool UserDetailExists(long id)
    {
      _logger.LogInformation("Checking Mobile Number is Same or Equal");

      return _context.UserDetails.Any(e => e.MobileNumber == id);
    }
  }
}
