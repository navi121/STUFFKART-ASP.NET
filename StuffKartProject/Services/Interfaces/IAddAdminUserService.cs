using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IAddAdminUserService
  {
    public Task<bool> AddAdminUser(UserDetails userDetail);
  }
}
