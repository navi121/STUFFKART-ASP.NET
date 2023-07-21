using StuffKartProject.Models;

namespace StuffKartProject.Services.Interfaces
{
  public interface IUserLoginService
  {
    public string ValidateUserAsync(UserDetails loginRequest);
  }
}
