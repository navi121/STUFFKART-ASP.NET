using StuffKartProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IGetOrderDetailsService
  {
    public Task<IEnumerable<OrderDetails>> SearchUserProduct(string userEmail);
  }
}
