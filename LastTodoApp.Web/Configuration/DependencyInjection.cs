using LastTodoApp.DataContext.Data;
using LastTodoApp.DataContext.Services;
using LastTodoApp.Domain.Entities;
using LastTodoApp.Web.Repositories.Services;
using LastTodoApp.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace LastTodoApp.Web.Configuration
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString) {


           services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.EnableSensitiveDataLogging();
            });
            return services;
        }

        public static IServiceCollection AddSwaggerAuthorizationMiddleware(this IServiceCollection services)
        {
            return services.AddScoped<SwaggerAuthorizationMiddleware>();
        }

        public static IServiceCollection AddSwaggerGen(this IServiceCollection services) {


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Todo API",
                    Version = "v1"
                });
            });

            return services;

        }

        public static IServiceCollection AddIdentity(this IServiceCollection services) {


            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;

        }

        public static IServiceCollection AddServicesAndRepositories(this IServiceCollection services)
        {

            services.AddScoped<AccountService>();
            services.AddScoped<ITaskRepository, TaskService>();
            services.AddScoped<AuditService>();
            services.AddAutoMapper(typeof(Program).Assembly);

            return services;
        }

        public static IServiceCollection AddAuthAndAuthorize(this IServiceCollection services)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";

    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("ADMIN"));
                options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("MANAGER"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("USER"));
            });

            return services;
        }

        // Add Default Controllers

        public static IServiceCollection AddDefaultControllers(this IServiceCollection services) {


            services.AddControllersWithViews();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            return services;

        }


    }
}
