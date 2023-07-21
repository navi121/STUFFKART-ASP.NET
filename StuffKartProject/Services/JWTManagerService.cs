using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StuffKartProject.Models;
using StuffKartProject.Resources;
using StuffKartProject.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StuffKartProject.Services
{
  public class JWTManagerService : IJWTManagerService
  {
    public IConfiguration Configuration { get; }
    public JWTManagerService(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public string Authenticate(UserDetails loginRequest)
    {
      var secretkey = Configuration.GetSection(WebResource.JwtKey).Value;
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.UTF8.GetBytes(secretkey);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Issuer = Configuration.GetSection(WebResource.JwtIssuer).Value,
        Audience = Configuration.GetSection(WebResource.JwtAudience).Value,
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Email, loginRequest.Email)
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}
