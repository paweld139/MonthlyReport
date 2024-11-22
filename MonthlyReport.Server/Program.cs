using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MonthlyReport.BLL.Extensions;
using MonthlyReport.BLL.Interfaces;
using MonthlyReport.BLL.Services;
using MonthlyReport.DAL;
using MonthlyReport.Server.Services;
using System.Text.Json.Serialization;

namespace MonthlyReport.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;

            var configuration = builder.Configuration;

            var environment = builder.Environment;

            AddServices(services, environment).Wait();

            AddDatabase(services, configuration);

            AddIdentity(services);

            var app = builder.Build();

            SeedDatabase(app).Wait();

            AddMiddlewares(app);

            app.MapControllers();

            app.MapRazorPages();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }

        private static async Task SeedDatabase(WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredService<MonthlyReportSeeder>();

            await seeder.Seed();
        }

        private static void AddDatabase(IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddSqlServer<MonthlyReportContext>(connectionString);
        }

        private static void AddIdentity(IServiceCollection services)
        {
            services
                .AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<MonthlyReportContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    if (context.HttpContext.Request.Path.StartsWithSegments("/api"))
                    {
                        context.Response.StatusCode = 401;
                    }
                    else
                    {
                        context.Response.Redirect(options.LoginPath);
                    }

                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToAccessDenied = context =>
                {
                    if (context.HttpContext.Request.Path.StartsWithSegments("/api"))
                    {
                        context.Response.StatusCode = 403;
                    }
                    else
                    {
                        context.Response.Redirect(options.AccessDeniedPath);
                    }

                    return Task.CompletedTask;
                };
            });
        }

        private static async Task AddServices(IServiceCollection services, IHostEnvironment hostEnvironment)
        {
            services
                .AddControllersWithViews()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddDataAnnotationsLocalization();

            services.AddRazorPages();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(MonthlyReportMappingProfile).Assembly);

            await hostEnvironment.PreparePuppeteerAsync();

            services.AddHttpContextAccessor();

            services.AddScoped<MonthlyReportSeeder>();

            services.AddScoped<IEntryService, EntryService>();

            services.AddScoped<IExportService, ExportService>();

            services.AddScoped<ITemplateService, RazorViewsTemplateService>();

            services.AddScoped<IHtmlToPdfConverter, HtmlToPdfConverter>();
        }

        private static void AddMiddlewares(WebApplication app)
        {
            app.UseDefaultFiles();

            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
        }
    }
}
