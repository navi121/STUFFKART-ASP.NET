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

    public async Task<bool> DeleteCartDetails(string email,CartDetail product)
    {
      var userDetail = _context.UserDetails.FirstOrDefault(x => x.Email == email);

      product.UserId = userDetail.UserId;

      //var productDetail = _context.CartDetails.Where(a => a.productName == product.productName && a.UserId == product.UserId && a.Total == product.Total && a.productDescription == product.productDescription).ToList();
      var productDetail = await _context.CartDetails.FirstOrDefaultAsync(a => a.productName == product.productName && a.UserId == product.UserId && a.Total == product.Total && a.productDescription == product.productDescription);
      //var product = await _context.CartDetails.FindAsync(userDetail.UserId);
      if (productDetail == null)
      {
        return false;
      }

        _context.CartDetails.Remove(productDetail);
        await _context.SaveChangesAsync();
      
      return true;
    }
  }
}
