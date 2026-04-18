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

// 🔹 Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MicroSys",
        Version = "v1"
    });

    // 🔐 JWT داخل Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter: Bearer {your token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,   // ✅ مهم
        Scheme = "Bearer",                // ✅ مهم
        BearerFormat = "JWT"
    });


    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});


var app = builder.Build();

// ========================
// ✅ Middleware
// ========================

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔐 مهم جدًا للـ JWT
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();