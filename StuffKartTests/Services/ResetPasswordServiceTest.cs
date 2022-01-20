using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests
{
  public class ResetPasswordServiceTest
  {
    private readonly StuffKartContext _mockContext;
    private readonly ResetPasswordService _resetPasswordService;
    private readonly Fixture _fixture = new Fixture();
    public ResetPasswordServiceTest()
    {
      _mockContext = new StuffKartContext();
      var _logger = new Mock<ILogger<ResetPasswordService>>();
      _resetPasswordService = new ResetPasswordService(_mockContext, _logger.Object);
    }

    [Fact]
    public async Task Given_Valid_Credentials_Login_Returns_true()
    {
      //Arrange
      var userDetailRequest = _fixture.Create<ResetPassword>();
      userDetailRequest.MobileNumber = 9080352867;
      userDetailRequest.SecurityAnswer = "palani";
      userDetailRequest.SecurityQuestion = "Which City You Have Born?";

      //Act
      var result = await _resetPasswordService.ValidateUserAsync(userDetailRequest.MobileNumber,userDetailRequest);

      //Assert
      Assert.True(result);
    }

    [Fact]
    public async Task Given_InValid_Credentials_Login_Returns_False()
    {
      //Arrange
      var userDetailRequest = _fixture.Create<ResetPassword>();

      //Act
      var result = await _resetPasswordService.ValidateUserAsync(userDetailRequest.MobileNumber, userDetailRequest);

      //Assert
      Assert.False(result);
    }
  }
}
