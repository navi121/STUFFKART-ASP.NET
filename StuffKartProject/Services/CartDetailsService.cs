using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class CartDetailsService : ICartDetailsService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;

    public CartDetailsService(StuffKartContext context, ILogger<CartDetailsService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<bool> AddCartDetails(string email, CartDetail cartDetail)
    {
      var userDetail = _context.UserDetails.FirstOrDefault(x => x.Email == email);
      cartDetail.UserId = userDetail.UserId;
      await _context.CartDetails.AddAsync(cartDetail);
      await _context.SaveChangesAsync();
      _logger.LogInformation("Added CartDetails");

      return true;
    }
  }
}
