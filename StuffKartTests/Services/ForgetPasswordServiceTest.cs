using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests
{
  public class ForgetPasswordServiceTest
  {
    private readonly StuffKartContext context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<ForgetPasswordService>> _logger;
    public ForgetPasswordServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<ForgetPasswordService>>();
    }

    [Fact]
    public async Task Given_Valid_CheckSecurity_Answer_Returns_True()
    {
      //Arrange
      var checkSecurityRequest = fixtureUserDetail();
      context.UserDetails.Add(checkSecurityRequest);
      await context.SaveChangesAsync();
      var service = new ForgetPasswordService(context,_logger.Object);

      //Act
      var result = await service.CheckUserEmail(checkSecurityRequest);

      //Assert
      Assert.True(result);
    }

    [Fact]
    public async Task Given_InValid_Email_Login_Returns_False()
    {
      //Arrange
      var checkSecurityRequest = fixtureUserDetail();
      var userGivenWrongDetail = fixtureUserDetail();
      context.UserDetails.Add(checkSecurityRequest);
      await context.SaveChangesAsync();
      var service = new ForgetPasswordService(context, _logger.Object);

      //Act
      var result = await service.CheckUserEmail(userGivenWrongDetail);

      //Assert
      Assert.False(result);
    }

    private UserDetails fixtureUserDetail()
    {
      return _fixture.Create<UserDetails>();
    }
  }
}
