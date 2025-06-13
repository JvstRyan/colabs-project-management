using Colabs.ProjectManagement.IntegrationTests.Factories;

namespace Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Fixtures
{
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<CustomWebApplicationFactory>
    {
    }
}
