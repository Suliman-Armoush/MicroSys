using Presentation.Middlewares;

namespace Presentation.SystemBuild
{
  public static class ApplicationPipeline
  {
    public static IApplicationBuilder UseApplicationPipeline(this IApplicationBuilder app)
    {
      app.UseMiddleware<ExceptionMiddleware>();

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MycroSys");
        c.DocumentTitle = "MycroSys";
      });

      app.UseHttpsRedirection();
      app.UseAuthentication();
      app.UseAuthorization();

      return app;
    }
  }
}
