using StuffKartProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface ISearchBarService
  {
   public Task<IEnumerable<UploadProducts>> SearchProduct(string productName);
  }
}
