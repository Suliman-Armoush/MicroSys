using Presentation.SystemBuild;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentationServices(builder.Configuration);

var app = builder.Build();

app.UseApplicationPipeline();
app.MapControllers();

app.Run();