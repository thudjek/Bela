using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Bela.WebMVC
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var jsonFileName = env == Environments.Development ? "appsettings.Development.json" : "appsettings.json";
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(jsonFileName)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            try
            {
                Log.Information("Application is starting up");
                var host = CreateHostBuilder(args).Build();
                //using (var scope = host.Services.CreateScope())
                //{
                //    var serviceProvider = scope.ServiceProvider;
                //    try
                //    {
                //        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                //        var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

                //        await DataSeeder.SeedData(userManager, roleManager);
                //    }
                //    catch (Exception ex)
                //    {
                //        Log.Error(ex, "Seeding data to database failed");
                //    }
                //}
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
