using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IOrderDetailsService
  {
    public Task<bool> PlaceOrder(string userId,List<OrderDetails> orderDetail);
  }
}
