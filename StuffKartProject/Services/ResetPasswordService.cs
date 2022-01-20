using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class ResetPasswordService : IResetPasswordService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;
    public ResetPasswordService(StuffKartContext context, ILogger<ResetPasswordService> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<bool> ValidateUserAsync(long MobileNumber, ResetPassword request)
    {
      var entity = _context.UserDetails.FirstOrDefault(x => x.MobileNumber == MobileNumber && x.SecurityAnswer == request.SecurityAnswer && x.SecurityQuestion == request.SecurityQuestion && x.Password!=request.Password && request.Password == request.ConfirmPassword);
      _logger.LogInformation("Checking Security Details");

      if (entity != null)
      {
        entity.Password = request.Password;
        await _context.SaveChangesAsync();
        _logger.LogInformation("Password Updated");

        return true;
      }
      else
      {
        _logger.LogWarning("There is some problem while updating password in ResetPasswordservice");

        return false;
      }
    }
  }
}
