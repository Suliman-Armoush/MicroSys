using Infrastructure.Persistence.Data;
using Presentation.SystemBuild;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentationServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            policy.WithOrigins(builder.Configuration["ApiBaseUrl"]) // رابط Blazor الخاص بك
                  .AllowAnyHeader()
                  .AllowAnyMethod();
                  //.AllowCredentials();
        });
});

// بعد app.Build()

var app = builder.Build();

app.UseApplicationPipeline();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    await DbSeeder.SeedAsync(db);
}

app.UseCors("AllowBlazor");

app.Run();