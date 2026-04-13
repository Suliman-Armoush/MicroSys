//using Application.Features.User;
using Application.Behaviors;
using Application.Features.Department.Command.Create;
using Application.Features.Department.Queries.GetById;
using Application.Helper;
using Application.Interfaces;
using FluentValidation;
using Infrastructure.Persistence.Data;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()); 

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);

    cfg.RegisterServicesFromAssembly(typeof(GetDepartmentByIdQuery).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));


});


builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly); 

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

        if (exception is FluentValidation.ValidationException validationException)
        {
            context.Response.StatusCode = 400;

            var errors = validationException.Errors
                .Select(e => new { field = e.PropertyName, message = e.ErrorMessage });

            await context.Response.WriteAsJsonAsync(errors);
        }
    });
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
