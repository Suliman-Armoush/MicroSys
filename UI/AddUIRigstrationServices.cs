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
            // إعداد HttpClient الأساسي مع عنوان الـ API
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseAddress) });

            // تسجيل خدمات Blazor
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMikrotikService, MikrotikService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IReportService, ReportService>();

            // يمكنك إضافة أي خدمات أخرى هنا مستقبلاً مثل IDepartmentService, IUserService إلخ.
        }
    }

}
