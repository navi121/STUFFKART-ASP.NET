using StuffKartProject.Models;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IUserDetailService
  {
    public Task<bool> UpdateUser(UserDetails updateRequest);
  }
}
