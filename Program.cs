using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Management_Tasks;
using Microsoft.Extensions.Logging;
using Management_Tasks.ApiServices;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("UserService", client =>
{
    client.BaseAddress = new Uri("https://localhost:7082/");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddHttpClient("TaskService", client =>
{
    client.BaseAddress = new Uri("https://localhost:7082/");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
// Ajouter le service UserService
builder.Services.AddScoped<UserService>();

// Configuration du niveau de journalisation
builder.Logging.SetMinimumLevel(LogLevel.Debug) // Permettre les logs à partir du niveau Debug
               .AddFilter("Microsoft", LogLevel.Warning); // Filtrer les logs Microsoft à partir du niveau Warning

await builder.Build().RunAsync();
