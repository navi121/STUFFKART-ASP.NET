using StuffKartProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IGetCartDetailsService
  {
    public Task<IEnumerable<CartDetail>> SearchUserProduct(string userEmail);
  }
}
