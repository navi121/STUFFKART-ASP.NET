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
  public class GetCartDetailsService : IGetCartDetailsService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;
    public GetCartDetailsService(StuffKartContext context, ILogger<GetCartDetailsService> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<IEnumerable<CartDetail>> SearchUserProduct(string userEmail)
    {
      var userId = _context.UserDetails.Where(x => x.Email == userEmail).FirstOrDefault();
      var searchResult = await _context.CartDetails.Where(m => m.UserId == userId.UserId).ToListAsync();

      _logger.LogInformation("Returning search Result");

      return searchResult;
    }
  }
}
