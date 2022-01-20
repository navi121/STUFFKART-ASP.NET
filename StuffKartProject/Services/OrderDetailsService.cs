using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class OrderDetailsService : IOrderDetailsService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;
    public OrderDetailsService(StuffKartContext context, ILogger<OrderDetailsService> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<bool> PlaceOrder(string userId,List<OrderDetails> orderDetail)
    {
      var userDetail = _context.UserDetails.FirstOrDefault(x => x.Email == userId);
      var n=0;
      while (n < orderDetail.Count)
      {
        orderDetail[n].UserId = userDetail.UserId;
        n++;
      }
      _context.Orders.AddRange(orderDetail);
      _logger.LogInformation("Getting userDetails from User : Line Number 23");
      
       await _context.SaveChangesAsync();
      _logger.LogInformation("Saved user given details Successfully : Line Number 32");

      return true;
    }
  }
}
