using AutoFixture;
using Microsoft.EntityFrameworkCore;
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
  public class UserDetailsServiceTest
  {
    private readonly StuffKartContext _context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<UserDetailService>> _logger;

    public UserDetailsServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      _context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<UserDetailService>>();
    }

    [Fact]
    public async Task UpdateUser_Service_Returns_true()
    {
      //Arrange
      var user = fixtureUser();
      _context.UserDetails.Add(user);
      await _context.SaveChangesAsync();
      var service = new UserDetailService(_context, _logger.Object);

      //Act
      bool result =await service.UpdateUser(user);

      //Assert
      Assert.True(result);
    }

    [Fact]
    public async Task UpdateUser_Service_Returns_false()
    {
      //Arrange
      var user = fixtureUser();
      _context.UserDetails.Add(user);
      await _context.SaveChangesAsync();
      var service = new UserDetailService(_context, _logger.Object);

      //Act
      bool result = await service.UpdateUser(fixtureUser());

      //Assert
      Assert.False(result);
    }
    private UserDetails fixtureUser()
    {
      return _fixture.Create<UserDetails>();
    }
  }
}
