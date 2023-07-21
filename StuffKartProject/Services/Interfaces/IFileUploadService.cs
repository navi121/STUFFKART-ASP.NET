using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace StuffKartProject.Services.Interfaces
{
  public interface IFileUploadService
  {
    //int ImageUpload(DashBoard id);
    public void ImageUpload(int id,List<IFormFile> file);
  }
}
