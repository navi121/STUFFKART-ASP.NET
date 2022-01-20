using Microsoft.AspNetCore.Mvc;
using StuffKartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Services.Interfaces
{
  public interface ISearchBarService
  {
   public Task<IEnumerable<UploadProducts>> SearchProduct(string productName);
  }
}
