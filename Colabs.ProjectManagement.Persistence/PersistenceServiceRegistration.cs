using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Colabs.ProjectManagement.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ColabsDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ColabsApplicationConnectionString")));
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(BaseRepository<>));
            
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}
