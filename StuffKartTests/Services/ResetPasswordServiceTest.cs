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
  public class ResetPasswordServiceTest
  {
    private readonly StuffKartContext _context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<ResetPasswordService>> _logger;
    public ResetPasswordServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      _context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<ResetPasswordService>>();
    }

    [Fact]
    public async Task Given_Valid_Credentials_Login_Returns_true()
    {
      //Arrange
      var userDetailRequest = randomUserDetail();
      var resetPassword = fixtureResetPassword(userDetailRequest);
      _context.UserDetails.Add(userDetailRequest);
      await _context.SaveChangesAsync();
      var service = new ResetPasswordService(_context,_logger.Object);

      //Act
      var result = await service.ValidateUserAsync(userDetailRequest.MobileNumber,resetPassword);

      //Assert
      Assert.True(result);
    }

    [Fact]
    public async Task Given_InValid_Credentials_Login_Returns_False()
    {
      //Arrange
      var userDetailRequest = randomUserDetail();
      var resetPassword = fixtureResetPassword(userDetailRequest);
      _context.UserDetails.Add(userDetailRequest);
      await _context.SaveChangesAsync();
      var service = new ResetPasswordService(_context, _logger.Object);
      userDetailRequest.MobileNumber = 1234;

      //Act
      var result = await service.ValidateUserAsync(userDetailRequest.MobileNumber, resetPassword);

      //Assert
      Assert.False(result);
    }

    private ResetPassword fixtureResetPassword(UserDetails userDetails)
    {
      var resetPassword = _fixture.Create<ResetPassword>();
      resetPassword.MobileNumber = userDetails.MobileNumber;
      resetPassword.SecurityAnswer = userDetails.SecurityAnswer;
      resetPassword.SecurityQuestion = userDetails.SecurityQuestion;
      resetPassword.ConfirmPassword = resetPassword.Password;

      return resetPassword;
    }
    private UserDetails randomUserDetail()
    {
      return _fixture.Create<UserDetails>();
    }
  }
}
