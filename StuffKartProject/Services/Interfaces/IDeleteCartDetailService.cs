using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IDeleteCartDetailService
  {
    public Task<bool> DeleteCartDetails(string User, CartDetail product);
  }
}
