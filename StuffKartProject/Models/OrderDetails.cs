using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Models
{
  public partial class OrderDetails
  {
    public int UserId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Price { get; set; }
    public char Size { get; set; }
    public long ZipCode { get; set; }
    public int Quantity { get; set; }
    public int Total { get; set; }
    public string Address { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public long MobileNumber { get; set; }
  }
}
