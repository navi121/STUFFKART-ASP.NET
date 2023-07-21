using StuffKartProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface ICategoryService
  {
    public Task<IEnumerable<UploadProducts>> DivideCategory(string productName);
  }
}
