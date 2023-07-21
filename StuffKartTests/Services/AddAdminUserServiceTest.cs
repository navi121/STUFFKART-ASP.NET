using AutoFixture;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class AddAdminUserServiceTest
  {
    private readonly StuffKartContext context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<AddAdminUserService>> _logger;
    public AddAdminUserServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<AddAdminUserService>>();
    }

    [Fact]
    public async Task Given_Valid_User_Details_To_Service_Returns_true()
    {
      //Arrange
      var service = new AddAdminUserService(context,_logger.Object);
      var user = fixtureUser();

      //Act
      var result = await service.AddAdminUser(user);

      //Assert
      Assert.True(result);
    }

    [Fact]
    public async Task Given_Invalid_User_Details_To_Service_Returns_false()
    {
      //Arrange
      var service = new AddAdminUserService(context, _logger.Object);
      var existingUser = fixtureUser();
      context.UserDetails.Add(existingUser);
      await context.SaveChangesAsync();
      var userDetailRequest = _fixture.Create<UserDetails>();
      userDetailRequest.MobileNumber = existingUser.MobileNumber;
      
      //Act
      var result = await service.AddAdminUser(userDetailRequest);

      //Assert
      Assert.False(result);
    }

    private UserDetails fixtureUser()
    {
      return _fixture.Create<UserDetails>();
    }
  }
}
