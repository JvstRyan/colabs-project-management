using Colabs.ProjectManagement.IntegrationTests.Factories;
using Colabs.ProjectManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Fixtures
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory _factory;
        
        public DatabaseFixture(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }
        
        public CustomWebApplicationFactory Factory => _factory;
        
        public async Task InitializeAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ColabsDbContext>();
            
            await dbContext.Database.EnsureCreatedAsync();

        }
        
        public async Task ResetDatabaseAsync()
        {
            // Reset database between tests
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ColabsDbContext>();
    
            // Get all entity types from the model
            var entityTypes = dbContext.Model.GetEntityTypes()
                .Where(e => !e.IsOwned()) // Skip owned entities
                .ToList();
    
            // Disable foreign key checks for PostgreSQL
            await dbContext.Database.ExecuteSqlRawAsync("SET session_replication_role = 'replica';");
    
            try
            {
                // Clear all tables
                foreach (var entityType in entityTypes)
                {
                    var tableName = entityType.GetTableName();
                    if (!string.IsNullOrEmpty(tableName))
                    {
                        // Using TRUNCATE is faster than DELETE for large tables
                        await dbContext.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE \"{tableName}\" RESTART IDENTITY CASCADE;");
                    }
                }
            }
            finally
            {
                // Re-enable foreign key checks
                await dbContext.Database.ExecuteSqlRawAsync("SET session_replication_role = 'origin';");
            }
        }


        public async Task DisposeAsync()
        {
        }
    }
}
