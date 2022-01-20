using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IGetCartDetailsService
  {
    public Task<IEnumerable<CartDetail>> SearchUserProduct(string userEmail);
  }
}
