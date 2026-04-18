using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;

using Presentation;
using Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// ========================
// ✅ Services
// ========================

builder.Services.AddPresentationServices(builder.Configuration);

var app = builder.Build();

// ========================
// ✅ Middleware
// ========================

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
          {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "MycroSys");
            c.DocumentTitle = "MycroSys";
          });
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();