using StuffKartProject.Models;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface ICartDetailsService
  {
    public Task<bool> AddCartDetails(string User,CartDetail cartDetail);
  }
}
