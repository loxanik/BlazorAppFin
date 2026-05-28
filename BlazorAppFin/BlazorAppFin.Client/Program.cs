using BlazorAppFin.Client.Services;
using BlazorAppFin.Client.Shared.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorAppFin.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            builder.Services.AddScoped<ITransactionService, BlazorTransactionService>();
            builder.Services.AddSingleton<ThemeState>();

            await builder.Build().RunAsync();
        }
    }
}
