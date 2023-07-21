using StuffKartProject.Models;

namespace StuffKartProject.Services.Interfaces
{
  public interface IJWTManagerService
  {
    public string Authenticate(UserDetails loginRequest);
  }
}
