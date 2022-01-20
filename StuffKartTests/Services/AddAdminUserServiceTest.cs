using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
using System.Threading.Tasks;
using Xunit;

namespace StuffKartTests.Services
{
  public class AddAdminUserServiceTest
  {
    private readonly StuffKartContext _mockContext;
    private readonly AddAdminUserService _addAdminUserService;
    private readonly Fixture _fixture = new Fixture();
    public AddAdminUserServiceTest()
    {
      _mockContext = new StuffKartContext();
      var _logger = new Mock<ILogger<AddAdminUserService>>();
      _addAdminUserService = new AddAdminUserService(_mockContext, _logger.Object);
    }
    [Fact]
    public async Task Given_Valid_User_Details_To_Service_Returns_true()
    {
      //Arrange
      var userDetailRequest = _fixture.Create<UserDetails>();
      userDetailRequest.UserId = 0;

      //Act
      var result = await _addAdminUserService.AddAdminUser(userDetailRequest);

      //Assert
      Assert.True(result);
    }

    [Fact]
    public async Task Given_Invalid_User_Details_To_Service_Returns_false()
    {
      //Arrange
      var userDetailRequest = _fixture.Create<UserDetails>();
      userDetailRequest.MobileNumber = 9080;

      //Act
      var result = await _addAdminUserService.AddAdminUser(userDetailRequest);

      //Assert
      Assert.False(result);
    }
  }
}
