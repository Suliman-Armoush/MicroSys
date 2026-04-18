using Application;
using Infrastructure;
using System.Runtime.CompilerServices;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services,  IConfiguration configuration)
        {
            services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //services.AddOpenApi();
            services.AddEndpointsApiExplorer();
          //  services.AddSwaggerGen();



            services.AddApplicationServices();
            services.AddInfrastructureServices(configuration);


            return services;
        }
    }
}
