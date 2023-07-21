using StuffKartProject.Models;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IAddUserDetailsService
  {
    public Task<bool> AddUser(UserDetails userDetail);
  }
}
