using StuffKartProject.Models;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IAddAdminUserService
  {
    public Task<bool> AddAdminUser(UserDetails userDetail);
  }
}
