using Diplom.Data;
using Diplom.Repositories.Implementation;
using Diplom.Repositories.Interface;
using Diplom.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Diplom.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        { 
            // Регистрация контекста базы данных AppDbContext с использованием строки подключения из конфигурации
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            });

            // Регистрация сервисов Identity для работы с пользователями и ролями
            services.AddIdentityCore<AppUser>(opt =>
            {
                // Настройка требований пароля пользователей
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
            })
                .AddRoles<IdentityRole>() // Регистрация ролей
                .AddRoleManager<RoleManager<IdentityRole>>() // Регистрация менеджера ролей
                .AddEntityFrameworkStores<AppDbContext>(); // Интеграция с базой данных через Entity Framework

            // Настройка аутентификации с использованием JWT Bearer-токенов
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            // Регистрация сервисов и репозиториев приложения    
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IDistrictRepo, DistrictRepo>();
            services.AddScoped<IRouteEnRepo, RouteEnRepo>();
            return services;
        }
    }
}
