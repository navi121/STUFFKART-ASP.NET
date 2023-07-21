using StuffKartProject.Models;

namespace StuffKartProject.Services.Interfaces
{
  public interface IAdminLoginService
  {
    string ValidateAdminUserAsync(UserDetails login);
  }
}
