using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StuffKartProject.Models;
using StuffKartProject.Services;
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
    private readonly StuffKartContext _mockContext;
    private readonly AdminLoginService _adminLoginService;
    private readonly Fixture _fixture = new Fixture();
    public AdminLoginServiceTest()
    {
      _mockContext = new StuffKartContext();
      var _logger = new Mock<ILogger<AdminLoginService>>();
      _adminLoginService = new AdminLoginService(_mockContext, _logger.Object);
    }

    [Fact]
    public async Task Given_Admin_Valid_Credentials_Login_Returns_ture()
    {
      //Arrange
      var userDetailRequest = _fixture.Create<UserDetails>();
      userDetailRequest.Email = "naveenchpt@gmail.com";
      userDetailRequest.Password = "nn2000";

      //Act
      var result = await _adminLoginService.ValidateAdminUserAsync(userDetailRequest);

      //Assert
      Assert.True(result);
    }

    [Fact]
    public async Task Given_Admin_InValid_Credentials_Login_Returns_false()
    {
      //Arrange
      var userDetailRequest = _fixture.Create<UserDetails>();

      //Act
      var result = await _adminLoginService.ValidateAdminUserAsync(userDetailRequest);

      //Assert
      Assert.False(result);
    }
  }
}
