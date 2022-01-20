using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services
{
  public class FileUploadService : IFileUploadService
  {
    private readonly StuffKartContext _context;
    private readonly ILogger _logger;
  
    public FileUploadService(StuffKartContext context, ILogger<FileUploadService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public void ImageUpload(int id, List<IFormFile> file)
    {
      int count = 0;
      var image = _context.Products.Where(m => m.ProductId == id).FirstOrDefault();
      _logger.LogInformation("chacking product Id is same as in Table");

      if (file != null)
      {
        if (file.Count > 0)
        {
          foreach (var formFile in file)
          {
            using (var ms = new MemoryStream())
            {
              formFile.CopyTo(ms);
              var img = ms.ToArray();
              string s = Convert.ToBase64String(img);              
              count++;
              if (count == 1)
              {
                image.Image = s;
              }
              else if (count == 2)
              {
                image.Image1 = s;
              }
              else
              {
                image.Image2 = s;
              }
            }
          }
          _context.SaveChanges();

          _logger.LogInformation("Image saved in DB");
        }
      }
    }
  }
}
