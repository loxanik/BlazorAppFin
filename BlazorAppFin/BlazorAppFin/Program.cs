using BlazorAppFin.Client.Shared.Interfaces;
using BlazorAppFin.Components;
using BlazorAppFin.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace BlazorAppFin
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddOpenApi();
            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var dbPath = Path.Combine(AppContext.BaseDirectory, "finance.bd");

                options.UseSqlite($"Data Source={dbPath}");
            });

            builder.Services.AddScoped<ITransactionService, BlazorAppFin.Client.Services.InMemoryTransactionService>();
            builder.Services.AddScoped<BlazorAppFin.Client.Services.ThemeState>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                await dbContext.Database.MigrateAsync();
            }

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
