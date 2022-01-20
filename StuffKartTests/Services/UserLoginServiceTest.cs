using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests
{
  public class UserLoginServiceTest
  {
    private readonly StuffKartContext _mockContext;
    private readonly UserLoginService _userLoginService;
    private readonly Fixture _fixture = new Fixture();
    public UserLoginServiceTest()
    {
      _mockContext = new StuffKartContext();
      var _logger = new Mock<ILogger<UserLoginService>>();
      _userLoginService = new UserLoginService(_mockContext, _logger.Object);
    }

    [Fact]
    public async Task Given_Valid_Credentials_Login_Returns_ture()
    {
      //Arrange
      var userDetailRequest = _fixture.Create<UserDetails>();
      userDetailRequest.Email = "naveenchpt@gmail.com";
      userDetailRequest.Password = "nn2000";

      //Act
      var result = await _userLoginService.ValidateUserAsync(userDetailRequest);

      //Assert
      Assert.True(result);
    }

    [Fact]
    public async Task Given_InValid_Credentials_Login_Returns_false()
    {
      //Arrange
      var userDetailRequest = _fixture.Create<UserDetails>();

      //Act
      var result = await _userLoginService.ValidateUserAsync(userDetailRequest);

      //Assert
      Assert.False(result);
    }
  }
}
