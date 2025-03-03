
using Guardian.Application.Contracts;
using Guardian.Persistence.DatabaseContext;
using Guardian.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Guardian.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // add database context
            services.AddDbContext<GuardianDatabaseContext>(options =>
            {
                options.UseSqlServer(configuration["DatabaseConnectionString"]);
            });

            // Add repository contracts and implementations
            services.AddScoped(typeof(IGenericRepository), typeof(UserRepository));

            return services;
        }
    }
}
