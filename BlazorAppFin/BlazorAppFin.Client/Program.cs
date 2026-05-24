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

            builder.Services.AddScoped<ITransactionService, InMemoryTransactionService>();

            await builder.Build().RunAsync();
        }
    }
}
