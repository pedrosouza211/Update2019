using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Services;

namespace SalesWebMvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Método chamado pelo runtime para adicionar serviços ao container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // MVC tradicional

            services.AddDbContext<SalesWebMvcContext>(options =>
            options.UseMySql(
            Configuration.GetConnectionString("SalesWebMvcContext"),
            new MySqlServerVersion(new Version(8, 0, 34)),
            mySqlOptions => mySqlOptions.MigrationsAssembly("SalesWebMvc")

            )
          );

            services.AddScoped<SeedingService>();
            services.AddScoped<SellerService>();
            services.AddScoped<DepartmentService>();

        }

        // Método chamado pelo runtime para configurar o pipeline HTTP
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
