using Microsoft.Extensions.Logging;
using StuffKartProject.Constant;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System.Linq;

namespace StuffKartProject.Services
{
  public class AdminLoginService : IAdminLoginService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;
    private readonly IJWTManagerService _tokenKey;

    public AdminLoginService(StuffKartContext context, ILogger<AdminLoginService> logger, IJWTManagerService tokenKey)
    {
      _logger = logger;
      _context = context;
      _tokenKey = tokenKey;
    }

    public string ValidateAdminUserAsync(UserDetails loginRequest)
    {

      var userExists = _context.UserDetails.Where(m => m.Email == loginRequest.Email && m.Password == loginRequest.Password).FirstOrDefault();
      _logger.LogInformation("Retruning user is Valid or Not");

      if (userExists == null || userExists.isAdmin != UserConstant.AdminUser)
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
