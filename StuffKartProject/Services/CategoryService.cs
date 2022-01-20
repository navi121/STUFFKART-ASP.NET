using Microsoft.EntityFrameworkCore;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class CategoryService : ICategoryService
  {
    private readonly StuffKartContext _context;

    public CategoryService(StuffKartContext context)
    {
      _context = context;
    }
    public async Task<IEnumerable<UploadProducts>> DivideCategory(string categoryName)
    {
      var categorySearch =await _context.Products.Where(m => m.Category == categoryName).ToListAsync();

      return categorySearch;
    }
  }
}
