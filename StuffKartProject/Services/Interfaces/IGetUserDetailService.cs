using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IGetUserDetailService
  {
    public Task<IEnumerable<UserDetails>> getUser(string email);
  }
}
