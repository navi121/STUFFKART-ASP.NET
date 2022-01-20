using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class ForgetPasswordService : IForgetPasswordService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;
    public ForgetPasswordService(StuffKartContext context, ILogger<ForgetPasswordService> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<bool> CheckUserEmail(UserDetails request)
    {
      var resetStatus = _context.UserDetails.Where(m => m.Email == request.Email).FirstOrDefault() != null;
      _logger.LogInformation("Checking Email Succesfull");

      return resetStatus;
    }
  }
}
