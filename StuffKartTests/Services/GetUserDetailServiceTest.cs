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
  public class GetUserDetailServiceTest
  {
    private readonly StuffKartContext _context;
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<GetUserDetailService>> _logger;

    public GetUserDetailServiceTest()
    {
      DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder<StuffKartContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
      _context = new StuffKartContext(dboptions.Options);
      _logger = new Mock<ILogger<GetUserDetailService>>();
    }

    [Fact]
    public async Task getUser_Service_Returns_Individual_UserDetail()
    {
      //Arrange
      var user = fixtureUser();
      _context.UserDetails.Add(user);
      await _context.SaveChangesAsync();
      var service = new GetUserDetailService(_context, _logger.Object);

      //Act
      var result =await service.getUser(user.Email);

      //Assert
      Assert.Equal(_context.UserDetails, result);
    }

    private UserDetails fixtureUser()
    {
      return _fixture.Create<UserDetails>();
    }
  }
}
