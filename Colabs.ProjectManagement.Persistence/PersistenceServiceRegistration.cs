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
            services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
            services.AddScoped<IWorkspaceInvitationRepository, WorkspaceInvitationRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IWorkspaceMemberRepository, WorkspaceMemberRepository>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<ISprintRepository, SprintRepository>();
            
            return services;
        }
    }
}
