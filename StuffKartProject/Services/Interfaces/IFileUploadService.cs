using Microsoft.AspNetCore.Http;
using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface IFileUploadService
  {
    //int ImageUpload(DashBoard id);
    public void ImageUpload(int id,List<IFormFile> file);
  }
}
