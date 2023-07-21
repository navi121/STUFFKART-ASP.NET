using StuffKartProject.Models;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IForgetPasswordService
  {
    Task<bool> CheckUserEmail(UserDetails request);
  }
}
