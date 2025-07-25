using System.Reflection;
using Colabs.ProjectManagement.Application.Contracts;
using Colabs.ProjectManagement.Domain.Common;
using Colabs.ProjectManagement.Domain.Entities;
using Colabs.ProjectManagement.Domain.Entities.Chat;
using Colabs.ProjectManagement.Domain.Entities.Documents;
using Colabs.ProjectManagement.Domain.Entities.TaskManagement;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Microsoft.EntityFrameworkCore;

namespace Colabs.ProjectManagement.Persistence
{
    public class ColabsDbContext : DbContext
    {
        private readonly ICurrentLoggedInUserService? _currentLoggedInUser;
        public ColabsDbContext(DbContextOptions<ColabsDbContext> options)
            : base(options)
        {
        }

        public ColabsDbContext(DbContextOptions<ColabsDbContext> options, ICurrentLoggedInUserService currentLoggedInUser) : base(options)
        {
            _currentLoggedInUser = currentLoggedInUser;
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        
        // Workspaces
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<WorkspaceInvitation> WorkspaceInvitations { get; set; }
        public DbSet<WorkspaceMember> WorkspaceMembers { get; set; }
        
        // Task Management
        public DbSet<Sprint> Sprints {get; set;}
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        
        // Documents
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentFolder> DocumentFolder { get; set; }
        
        // Chat
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomMember> ChatRoomMembers { get; set; }
        public DbSet<ChatMessage> ChatMessage { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColabsDbContext).Assembly);
        }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                var now = DateTime.UtcNow;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = now;
                        entry.Entity.CreatedBy = _currentLoggedInUser?.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = now;
                        entry.Entity.LastModifiedBy = _currentLoggedInUser?.UserId;
                        break;
                
                }
            }

            return await base.SaveChangesAsync(cancellationToken);

        }
    }
}
