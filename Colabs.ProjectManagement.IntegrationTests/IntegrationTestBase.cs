using Colabs.ProjectManagement.IntegrationTests.Factories;
using Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Fixtures;
using Microsoft.Extensions.DependencyInjection;

namespace Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage
{
    public abstract class IntegrationTestBase : IAsyncLifetime
    {
        protected readonly HttpClient Client;
        protected readonly CustomWebApplicationFactory Factory;
        private readonly DatabaseFixture _databaseFixture;

        
        protected IntegrationTestBase(CustomWebApplicationFactory factory)
        {
            Factory = factory;
            Client = factory.CreateClient();
            _databaseFixture = new DatabaseFixture(factory);
        }
        
        protected T GetService<T>() where T : class
        {
            return Factory.Services.GetRequiredService<T>();
        }


        public async Task InitializeAsync()
        {
            await _databaseFixture.InitializeAsync();
            await _databaseFixture.ResetDatabaseAsync();

        }

        public async Task DisposeAsync()
        {
        }
    }
}
