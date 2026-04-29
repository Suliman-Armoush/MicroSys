using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// استدعاء طريقة التسجيل مع تمرير رابط الـ API
builder.Services.AddUIServices("https://localhost:54982"); // ضع عنوان API الخاص بك

await builder.Build().RunAsync();