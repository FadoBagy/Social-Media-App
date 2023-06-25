namespace Social_Media_App
{
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Social_Media_App.Data;
    using Social_Media_App.Data.Models;
    using Social_Media_App.Infrastructure;
    using Social_Media_App.Services.Email;
    using Social_Media_App.Services;
    using System.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;
            var environment = builder.Environment;

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(configuration);

            configuration
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("Data/appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"Data/appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>(IdentityOptionsProvider.GetIdentityOptions)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            }).AddRazorRuntimeCompilation();
        }

        private static void Configure(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();

                if (!dbContext.Chats.Any())
                {
                    DatabaseGenerator.GenerateAsync(dbContext).GetAwaiter().GetResult();
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
        }
    }
}