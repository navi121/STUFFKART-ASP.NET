using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StuffKart.AcceptanceTests;
using StuffKartProject.Models;
using StuffKartProject.Services;
using StuffKartProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuffKartProject.AcceptanceTests.Helpers
{
  internal class TestStartup : Startup
  {

    public TestStartup(IConfiguration configuration): base(configuration) { }
    public override void ConfigureServices(IServiceCollection services)
    {
      var a=GetAppSettings.AppSetting;
      string connectionString = a.GetConnectionString("BloggingDatabase");
      services.AddDbContext<StuffKartContext>(options => options.UseSqlServer(connectionString));

      string secretKey = a.GetSection("Jwt:Key").Value;

      var jwttokenparams = new TokenValidationParameters()
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = a.GetSection("Jwt:Issuer").Value,
        ValidAudience = a.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
      };

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(jwtconfig =>
              jwtconfig.TokenValidationParameters = jwttokenparams);

      services.AddDbContext<StuffKartContext>();
      services.AddScoped<IForgetPasswordService, ForgetPasswordService>();
      services.AddScoped<IFileUploadService, FileUploadService>();
      services.AddScoped<IUserLoginService, UserLoginService>();
      services.AddScoped<IAddAdminUserService, AddAdminUserService>();
      services.AddScoped<IAdminLoginService, AdminLoginService>();
      services.AddScoped<ISearchBarService, SearchBarService>();
      services.AddScoped<IResetPasswordService, ResetPasswordService>();
      services.AddScoped<IAddUserDetailsService, AddUserDetailsService>();
      services.AddScoped<ICartDetailsService, CartDetailsService>();
      services.AddScoped<ICategoryService, CategoryService>();
      services.AddScoped<IDashBoardService, DashBoardService>();
      services.AddScoped<IOrderDetailsService, OrderDetailsService>();
      services.AddScoped<IGetOrderDetailsService, GetOrderDetailsService>();
      services.AddScoped<IGetCartDetailsService, GetCartDetailsService>();
      services.AddScoped<IUserDetailService, UserDetailService>();
      services.AddScoped<IGetUserDetailService, GetUserDetailService>();
      services.AddSingleton<JWTManagerService>();
      services.AddControllers();

      base.ConfigureServices(services);
    }

    public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      base.Configure(app, env);
    }
  }
}
