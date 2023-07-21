using StuffKartProject.Models;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IDashBoardService
  {
    public Task<int> AddProductDetailService(UploadProducts addproduct);
  }
}
