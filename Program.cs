using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EventManagementSystem;
using EventManagementSystem.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient for potential API calls
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register application services with Scoped lifetime
// Scoped services are created once per user session in Blazor WebAssembly
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<RegistrationService>();

await builder.Build().RunAsync();
