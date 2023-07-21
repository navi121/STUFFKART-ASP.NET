using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System.Linq;

namespace StuffKartProject.Services
{
  public class UserLoginService : IUserLoginService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;
    private readonly IJWTManagerService _tokenKey;
    public UserLoginService(StuffKartContext context, ILogger<UserLoginService> logger, IJWTManagerService tokenKey)
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

      _logger.LogInformation("Returning token");

      return token;
    }
  }
}
