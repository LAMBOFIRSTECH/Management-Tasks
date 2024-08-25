using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Management_Tasks; 
using Blazor.Extensions.Logging;
using Management_Tasks.ApiServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<UserService>();

builder.Logging.AddBrowserConsole()
	.SetMinimumLevel(LogLevel.Debug) //Setting LogLevel is optional
	.AddFilter("Microsoft", LogLevel.Information); 
	
builder.Services.AddScoped(sp =>
	new HttpClient
	{
		BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)  // A revoir 
	});

await builder.Build().RunAsync();
