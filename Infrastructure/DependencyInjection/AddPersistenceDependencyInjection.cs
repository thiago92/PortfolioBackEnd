using Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DependencyInjection
{
    public static class AddPersistenceDependencyInjection
    {
        public static IServiceCollection PersistenceDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(10, 5, 0))
                );
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });

            services.RepositorysDependencyInjection();

            return services;
        }   
    }
}
