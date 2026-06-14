using Infrastructure.Persistence.Data;
using Presentation.SystemBuild;

var builder = WebApplication.CreateBuilder(args);

// تسجيل الخدمات
builder.Services.AddPresentationServices(builder.Configuration);

// إعداد سياسة CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            // تأكد من أن "ApiBaseUrl" في appsettings.json تساوي "http://192.168.0.19:7076"
            // إذا استمرت المشكلة، جرب وضع الرابط مباشرة كنص داخل WithOrigins
            //  policy.WithOrigins(builder.Configuration["ApiBaseUrl"] ?? "http://192.168.0.19:7076")
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// ترتيب الـ Pipeline ضروري جداً
// يجب أن يكون UseCors في البداية
app.UseCors("AllowBlazor");

// بقية المعالجات
app.UseApplicationPipeline();

// تهيئة قاعدة البيانات
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    await DbSeeder.SeedAsync(db);
}

app.MapControllers();

app.Run();