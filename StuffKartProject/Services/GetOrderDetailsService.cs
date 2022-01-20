using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class GetOrderDetailsService : IGetOrderDetailsService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;
    public GetOrderDetailsService(StuffKartContext context, ILogger<GetOrderDetailsService> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<IEnumerable<OrderDetails>> SearchUserProduct(string userEmail)
    {
      var userId = _context.UserDetails.Where(x => x.Email == userEmail).FirstOrDefault();
      var searchResult = await _context.Orders.Where(m => m.UserId == userId.UserId).ToListAsync();

      _logger.LogInformation("Returning search Result");

      return searchResult;
    }
  }
}
