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
  public class DashBoardService : IDashBoardService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;

    public DashBoardService(StuffKartContext context, ILogger<DashBoardService> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<int> AddProductDetailService(UploadProducts addproduct)
    {
      _logger.LogInformation("Receiving Products in Service");
      addproduct.Image = "n";
      addproduct.Image1 = "n";
      addproduct.Image2 = "n";
      addproduct.Quantity = 1;
      addproduct.Total = addproduct.Price;
      _context.Products.Add(addproduct);
      await _context.SaveChangesAsync();

      _logger.LogInformation("Product saved Succesfully");

      return addproduct.ProductId;
    }
  }
}
