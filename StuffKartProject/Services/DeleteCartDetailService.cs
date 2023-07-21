using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class DeleteCartDetailService : IDeleteCartDetailService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;

    public DeleteCartDetailService(StuffKartContext context, ILogger<DeleteCartDetailService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<bool> DeleteCartDetails(string email, CartDetail product)
    {
      var userDetail = _context.UserDetails.FirstOrDefault(x => x.Email == email);
      _logger.LogInformation("Check the user's email with database");

      product.UserId = userDetail.UserId;
      _logger.LogInformation("get user's userID and assigning it to cart product userId");

      var productDetail = await _context.CartDetails.FirstOrDefaultAsync(a => a.productName == product.productName &&
                          a.UserId == product.UserId && a.Total == product.Total &&
                          a.productDescription == product.productDescription);
      _logger.LogInformation("checking coming product that match with existing product and asign it to productdetail");

      if (productDetail == null)
      {
        _logger.LogError("product not matching and returning false");

        return false;

      }
      _logger.LogInformation("product found and removed from DB");
      _context.CartDetails.Remove(productDetail);

      await _context.SaveChangesAsync();
      _logger.LogInformation("product removed and saved Database");

      return true;
    }
  }
}
