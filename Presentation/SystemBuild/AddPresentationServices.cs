using Application;
using Infrastructure;
using Microsoft.OpenApi;

namespace Presentation.SystemBuild
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddControllers();
      // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
      services.AddEndpointsApiExplorer();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "MicroSys",
          Version = "v1",
          Description = "featured API",
          Contact = new OpenApiContact
          {
            Name = "Suliman Armoush",
            Email = "suliman221232@gmail.com"
          }
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = "Enter: {your token} without {Bearer}",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.Http, 
          Scheme = "Bearer",               
          BearerFormat = "JWT"

        });


        c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        {
          [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
      });

      services.AddApplicationServices();
      services.AddInfrastructureServices(configuration);
      services.AddHttpContextAccessor();


      return services;
    }
  }
}
