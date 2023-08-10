using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using core_strength_yoga_products_management.Data;
using core_strength_yoga_products_management.Areas.Identity.Data;

namespace core_strength_yoga_products_management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("core_strength_yoga_products_managementContextConnection") ?? throw new InvalidOperationException("Connection string 'core_strength_yoga_products_managementContextConnection' not found.");

            builder.Services.AddDbContext<core_strength_yoga_products_managementContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<core_strength_yoga_products_managementUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<core_strength_yoga_products_managementContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

            app.Run();
        }
    }
}