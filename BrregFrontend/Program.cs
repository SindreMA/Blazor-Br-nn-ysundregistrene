using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BrregFrontend;
using MudBlazor.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient(new CookieHandler()) { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add configuration service
builder.Services.AddScoped<ConfigurationService>();

var host = builder.Build();

// Load configuration before running the app
var configService = host.Services.GetRequiredService<ConfigurationService>();
var httpClient = host.Services.GetRequiredService<HttpClient>();
var config = await configService.GetConfigurationAsync();

// Set the API URL in Statics
Statics.apiUrl = config.ApiUrl;

await host.RunAsync();
