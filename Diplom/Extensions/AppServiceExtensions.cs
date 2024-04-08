using Diplom.Data;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            });

            return services;
        }
    }
}
