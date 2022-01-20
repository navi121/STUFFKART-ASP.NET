using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StuffKartProject.Models;
using StuffKartProject.Services;
using StuffKartProject.Services.Interfaces;
using System.Data.Common;
using System.Text;
using System.Text.Json.Serialization;

namespace StuffKartProject
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public virtual void ConfigureServices(IServiceCollection services)
    {

      string connectionString = Configuration.GetConnectionString("BloggingDatabase");
      services.AddDbContext<StuffKartContext>(options => options.UseSqlServer(connectionString));

      string secretKey = Configuration.GetSection("Jwt:Key").Value;

      var jwttokenparams = new TokenValidationParameters()
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = Configuration.GetSection("Jwt:Audience").Value,
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
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "StuffKartProject", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StuffKartProject v1"));
      }
      app.UseCors(options =>
      options.WithOrigins("http://localhost:4200")
      .AllowAnyMethod()
      .AllowAnyHeader());

      app.UseRouting();

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
