using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using StuffKartProject;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class JWTManagerServiceTest
  {
    private readonly Fixture _fixture = new();
    IConfiguration configuration;
    public JWTManagerServiceTest()
    {
      var inMemorySettings = new Dictionary<string, string> {
        {"Jwt:Issuer", "http://localhost.com"},
        {"Jwt:Key", "this is a Private key"},
        {"Jwt:Audience","http://localhost.com" }
      };
      configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(inMemorySettings)
        .Build();
    }

    [Fact]
    public void Authenticate_method_returns_token()
    {
      //Arrange
      var user = _fixture.Create<UserDetails>();
      var jwtToken = new JWTManagerService(configuration);

      //Act
      var result = jwtToken.Authenticate(user);

      //Assert
      Assert.True(result.Length > 50);
    }
  }
}
