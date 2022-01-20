using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class UserLoginService : IUserLoginService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;
    private readonly JWTManagerService _tokenKey;
    public UserLoginService(StuffKartContext context, ILogger<UserLoginService> logger, JWTManagerService tokenKey)
    {
      _context = context;
      _logger = logger;
      _tokenKey = tokenKey;
    }
    public string ValidateUserAsync(UserDetails loginRequest)
    {
      var userExists = _context.UserDetails.Where(m => m.Email == loginRequest.Email && m.Password == loginRequest.Password).FirstOrDefault();
      _logger.LogInformation("Retruning user is Valid or Not");
      if (userExists == null)
      {
        return null;
      }

      var token = _tokenKey.Authenticate(userExists);

      return token;
    }
  }
}
