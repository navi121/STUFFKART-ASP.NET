using Microsoft.EntityFrameworkCore;
using StuffKartProject.Models;
using StuffKartProject.Resources;
using StuffKartProject.Services.Interfaces;
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
      if(categoryName == WebResource.All)
      {
        return await _context.Products.ToListAsync();
      }
      var categorySearch =await _context.Products.Where(m => m.Category == categoryName).ToListAsync();

      return categorySearch;
    }
  }
}
