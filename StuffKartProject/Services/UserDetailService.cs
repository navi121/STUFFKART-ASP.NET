using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class UserDetailService : IUserDetailService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;

    public UserDetailService(StuffKartContext context, ILogger<UserDetailService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<bool> UpdateUser(UserDetails updateRequest)
    {

      var gettingUserDetail = _context.UserDetails.Where(m => m.Email == updateRequest.Email).FirstOrDefault();

      if (gettingUserDetail != null)
      {
        gettingUserDetail.FirstName = updateRequest.FirstName;
        gettingUserDetail.LastName = updateRequest.LastName;
        gettingUserDetail.MobileNumber = updateRequest.MobileNumber;
        gettingUserDetail.Email = updateRequest.Email;
        gettingUserDetail.SecurityQuestion = updateRequest.SecurityQuestion;
        gettingUserDetail.SecurityAnswer = updateRequest.SecurityAnswer;


        //gettingUserDetail = updateRequest;

        await _context.SaveChangesAsync();

        return true;
      }

      return false;


    }
  }
}
