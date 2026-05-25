using BlazorAppFin.Client.Shared.Interfaces;
using BlazorAppFin.Components;
using BlazorAppFin.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace BlazorAppFin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddOpenApi();
            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options => 
                options.UseSqlite("Data Source=finance.db"));

            builder.Services.AddScoped<ITransactionService, BlazorAppFin.Client.Services.InMemoryTransactionService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.MapOpenApi();
                app.MapScalarApiReference();   
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapControllers();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client.Layout.MainLayout).Assembly);

            app.Run();
        }
    }
}
