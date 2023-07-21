using StuffKartProject.Models;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IResetPasswordService
  {
    Task<bool> ValidateUserAsync(long MobileNumber,ResetPassword loginRequest);
  }
}
