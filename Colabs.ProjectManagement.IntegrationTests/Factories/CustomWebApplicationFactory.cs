using Colabs.ProjectManagement.Api;
using Colabs.ProjectManagement.Persistence;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Testcontainers.Azurite;
using Testcontainers.PostgreSql;

namespace Colabs.ProjectManagement.IntegrationTests.Factories
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly AzuriteContainer _azuriteContainer;
        private readonly PostgreSqlContainer _postgreSQLContainer;
        private readonly string _dbName = $"TestDb_{Guid.NewGuid().ToString().Replace("-", "_")}";


        public CustomWebApplicationFactory()
        {
            _azuriteContainer = new AzuriteBuilder()
                .WithImage("mcr.microsoft.com/azure-storage/azurite:latest")
                .Build();
            
            _postgreSQLContainer = new PostgreSqlBuilder()
                .WithImage("postgres:15")
                .WithDatabase("testdb")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .Build();
                
        }

        public async Task InitializeAsync()
        { 
            await _postgreSQLContainer.StartAsync();
            await _azuriteContainer.StartAsync();
            
            using var connection = new NpgsqlConnection(_postgreSQLContainer.GetConnectionString());
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            
            // Use double quotes around the database name to properly escape it
            command.CommandText = $"CREATE DATABASE \"{_dbName}\"";
            await command.ExecuteNonQueryAsync();
            
            // Apply schema to the new database
            using var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ColabsDbContext>();
            await dbContext.Database.EnsureCreatedAsync();

        }

        public async Task DisposeAsync()
        {
            await _azuriteContainer.StopAsync();
            await _postgreSQLContainer.StopAsync();
            await base.DisposeAsync();

        }

        public ColabsDbContext CreateDbContext()
        {
            var scope = Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<ColabsDbContext>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                int blobPort = _azuriteContainer.GetMappedPublicPort(10000);

                configBuilder.AddInMemoryCollection(new Dictionary<string, string>
                {
                   
                            ["BlobStorage:ConnectionString"] = 
                            $"DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:{blobPort}/devstoreaccount1;",
                            
                            ["ConnectionStrings:DefaultConnection"] = $"{_postgreSQLContainer.GetConnectionString()};Database={_dbName}",

                            
                            ["JwtSettings:Key"] = "your-test-jwt-key-should-be-at-least-16-characters-long",
                            ["JwtSettings:ExpiryInDays"] = "7",
                            ["JwtSettings:Issuer"] = "test-issuer",
                            ["JwtSettings:Audience"] = "test-audience"


                });
            });
            
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ColabsDbContext>));
                
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                
                var connectionString = $"{_postgreSQLContainer.GetConnectionString()};Database={_dbName}";


                services.AddDbContext<ColabsDbContext>(options =>
                {
                    options.UseNpgsql(connectionString);
                    options.EnableSensitiveDataLogging();

                });
            });
        }
    }
}
