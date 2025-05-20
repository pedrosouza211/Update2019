using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;

namespace SalesWebMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seedingService = services.GetRequiredService<SeedingService>();
                seedingService.Seed(); 
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}