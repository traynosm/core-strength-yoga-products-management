using core_strength_yoga_products_management.Areas.Identity.Data;
using core_strength_yoga_products_management.Data;
using core_strength_yoga_products_management.Interfaces;
using core_strength_yoga_products_management.Services;
using core_strength_yoga_products_management.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = builder.Configuration.GetConnectionString(
                "core_strength_yoga_products_managementContextConnection") ?? 
                throw new InvalidOperationException(
                    "Connection string " +
                    "'core_strength_yoga_products_managementContextConnection' not found.");

            builder.Services.AddDbContext<core_strength_yoga_products_managementContext>(options => 
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<core_strength_yoga_products_managementUser>(options => 
                options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<core_strength_yoga_products_managementContext>();

            IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<core_strength_yoga_products_managementUser>();

            identityBuilder = new IdentityBuilder(identityBuilder.UserType, builder.Services);

            identityBuilder.AddEntityFrameworkStores<core_strength_yoga_products_managementContext>();

            identityBuilder.AddSignInManager<SignInManager<core_strength_yoga_products_managementUser>>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient<IProductService, ProductService>();
            builder.Services.AddSingleton<ILoginService, LoginService>();

            builder.Services.Configure<ApiSettings>(o =>
                configuration.GetSection("ApiSettings").Bind(o));

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

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