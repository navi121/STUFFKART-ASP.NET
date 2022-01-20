using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests
{
  public class ForgetPasswordServiceTest
  {
    private readonly StuffKartContext _mockContext;
    private readonly ForgetPasswordService _checkSecurityAnswerService;
    private readonly Fixture _fixture = new Fixture();
    public ForgetPasswordServiceTest()
    {
      _mockContext = new StuffKartContext();
      var _logger = new Mock<ILogger<ForgetPasswordService>>();
      _checkSecurityAnswerService = new ForgetPasswordService(_mockContext, _logger.Object);
    }

    [Fact]
    public async Task Given_Valid_CheckSecurity_Answer_Returns200Ok()
    {
      //Arrange
      var checkSecurityRequest = _fixture.Create<UserDetails>();
      checkSecurityRequest.Email = "naveenchpt@gmail.com";

      //Act
      var result = await _checkSecurityAnswerService.CheckUserEmail(checkSecurityRequest);

      //Assert
      Assert.True(result);
    }

    [Fact]
    public async Task Given_InValid_Email_Login_Returns400BadRequest()
    {
      //Arrange
      var checkSecurityRequest = _fixture.Create<UserDetails>();

      //Act
      var result = await _checkSecurityAnswerService.CheckUserEmail(checkSecurityRequest);

      //Assert
      Assert.False(result);
    }
  }
}
