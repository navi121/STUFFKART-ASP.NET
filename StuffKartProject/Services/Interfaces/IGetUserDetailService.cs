using StuffKartProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IGetUserDetailService
  {
    public Task<IEnumerable<UserDetails>> getUser(string email);
  }
}
