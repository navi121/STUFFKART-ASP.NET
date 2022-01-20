using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StuffKartProject.Models;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Controllers
{

  [Route("AddProduct")]
  [ApiController]
  public class DashBoardController : ControllerBase
  {
    private readonly IDashBoardService _dashBoardService;
    private readonly ILogger _logger;
    public DashBoardController(IDashBoardService dashBoardService, ILogger<DashBoardController> logger)
    {
      _dashBoardService = dashBoardService;
      _logger = logger;
    }

    [HttpPost]
    public async Task<int> AddProductDetail(UploadProducts addproduct)
    {
      try
      {
        var productId =await _dashBoardService.AddProductDetailService(addproduct);
        _logger.LogInformation("Receiving Products Succesfully and Added to Table");

        return productId;
      }
      catch (Exception)
      {
        _logger.LogWarning("Received Exception Error while running");

        return 0;
      }
    }
  }
}
