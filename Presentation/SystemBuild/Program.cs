using Infrastructure.Persistence.Data;
using Presentation.SystemBuild;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentationServices(builder.Configuration);
;
var app = builder.Build();

app.UseApplicationPipeline();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    await DbSeeder.SeedAsync(db);
}

app.Run();