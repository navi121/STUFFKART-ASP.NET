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
  public class SearchBarService : ISearchBarService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;
    public SearchBarService(StuffKartContext context, ILogger<SearchBarService> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<IEnumerable<UploadProducts>> SearchProduct(string productName)
    {
      var searchResult = await _context.Products.Where(m => m.ProductName == productName).ToListAsync();
      _logger.LogInformation("Checking Product Name with DB");

      var notFound =await _context.Products.ToListAsync();

      if (searchResult.Count != 0)
      {
        _logger.LogInformation("Returning search Result");
        return searchResult;
      }
      _logger.LogWarning("Product not found and Return all the products");

      return notFound;
    }
  }
}
