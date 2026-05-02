using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;
using UI.Services;
using UI.Services.Interfaces;
using UI.Services.Reposetories;

namespace UI
{

  public static class UIServiceRegistration
  {
    public static void AddUIServices(this IServiceCollection services, string apiBaseAddress)
    {
      // Configure the shared HttpClient with the API base address.
      services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseAddress) });

      // Register Blazor and application services.
      services.AddBlazoredLocalStorage();
      services.AddAuthorizationCore();
      services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<IRoleService, RoleService>();
      services.AddScoped<IMikrotikService, MikrotikService>();
      services.AddScoped<IDepartmentService, DepartmentService>();
      services.AddScoped<IReportService, ReportService>();
      services.AddScoped<ISysInfosService, SysInfosService>();
      services.AddScoped<IUserService, UserService>();

      // Add future UI services here when needed.
    }
  }

}
