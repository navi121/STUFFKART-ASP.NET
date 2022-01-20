using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.IO;

namespace StuffKart.AcceptanceTests
{
  public class GetAppSettings
  {
    public static IConfiguration AppSetting { get; }
    static GetAppSettings()
    {
      AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
    }
    //public static void Main(string[] args)
    //{
    //  Console.WriteLine("Hello World!");
    //}
  }
}
