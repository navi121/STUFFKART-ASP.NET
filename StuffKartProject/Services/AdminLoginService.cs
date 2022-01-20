using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class AdminLoginService : IAdminLoginService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;
    private readonly JWTManagerService _tokenKey;

    public AdminLoginService(StuffKartContext context, ILogger<AdminLoginService> logger, JWTManagerService tokenKey)
    {
      _logger = logger;
      _context = context;
      _tokenKey = tokenKey;
    }

    public string ValidateAdminUserAsync(UserDetails loginRequest)
    {

      var userExists = _context.UserDetails.Where(m => m.Email == loginRequest.Email && m.Password == loginRequest.Password).FirstOrDefault();
      _logger.LogInformation("Retruning user is Valid or Not");

      if (userExists == null || userExists.isAdmin != 1)
      {
        _logger.LogInformation("Checking user is Admin or Not");

        return null;
      }
      _logger.LogWarning("Retruning user is not an Admin user");

      var token = _tokenKey.Authenticate(userExists);

      return token;
    }
  }
}
