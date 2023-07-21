using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject;
using StuffKartProject.Models;
using StuffKartProject.Services;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class AdminLoginServiceTest
  {
    private readonly StuffKartContext context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<AdminLoginService>> _logger;
    private readonly Mock<IJWTManagerService> _JWTService = new();

    public AdminLoginServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<AdminLoginService>>();
    }

    [Fact]
    public async Task Given_Admin_Valid_Credentials_Login_Returns_token()
    {
      //Arrange
      var adminUser = fixtureUser();
      adminUser.isAdmin = 1;
      context.UserDetails.Add(adminUser);
      await context.SaveChangesAsync();
      var service = new AdminLoginService(context, _logger.Object,_JWTService.Object);

      //Act
      _JWTService.Setup(x => x.Authenticate(adminUser)).Returns("token");
      var result = service.ValidateAdminUserAsync(adminUser);

      //Assert
      Assert.NotNull(result);
    }

    [Fact]
    public async Task Given_Admin_InValid_Credentials_Login_Returns_Null()
    {
      //Arrange
      var adminUser = fixtureUser();
      adminUser.isAdmin = 1;
      context.UserDetails.Add(adminUser);
      await context.SaveChangesAsync();
      var service = new AdminLoginService(context, _logger.Object, _JWTService.Object);

      //Act
      var result = service.ValidateAdminUserAsync(adminUser);

      //Assert
      Assert.Null(result);
    }

    private UserDetails fixtureUser()
    {
      return _fixture.Create<UserDetails>();
    }
  }
}
