using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface ICartDetailsService
  {
    public Task<bool> AddCartDetails(string User,CartDetail cartDetail);
    public Task<bool> DeleteCartDetails(string User,CartDetail product);
  }
}
