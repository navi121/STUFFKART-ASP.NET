using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using StuffKartProject.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests
{
  public class UserLoginServiceTest
  {
    private readonly StuffKartContext context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<UserLoginService>> _logger;
    private readonly Mock<IJWTManagerService> _JWTService = new();
    public UserLoginServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<UserLoginService>>();
    }

    [Fact]
    public async Task Given_Valid_Credentials_Login_Returns_Token()
    {
      //Arrange
      var user = fixtureUser();
      context.UserDetails.Add(user);
      await context.SaveChangesAsync();
      var service = new UserLoginService(context, _logger.Object, _JWTService.Object);

      //Act
      _JWTService.Setup(x => x.Authenticate(user)).Returns("token");
      var result = service.ValidateUserAsync(user);

      //Assert
      Assert.NotNull(result);
    }

    [Fact]
    public async Task Given_InValid_Credentials_Login_Returns_Null()
    {
      //Arrange
      var user = fixtureUser();
      context.UserDetails.Add(user);
      await context.SaveChangesAsync();
      var service = new UserLoginService(context, _logger.Object, _JWTService.Object);

      //Act
      var result = service.ValidateUserAsync(user);

      //Assert
      Assert.Null(result);
    }

    private UserDetails fixtureUser()
    {
      return _fixture.Create<UserDetails>();
    }
  }
}
