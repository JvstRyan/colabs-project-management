using Colabs.ProjectManagement.IntegrationTests.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage
{
    public abstract class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory>
    {
        protected readonly HttpClient Client;
        protected readonly CustomWebApplicationFactory Factory;
        
        protected IntegrationTestBase(CustomWebApplicationFactory factory)
        {
            Factory = factory;
            Client = factory.CreateClient();
        }
        
        protected T GetService<T>() where T : class
        {
            return Factory.Services.GetRequiredService<T>();
        }


    }
}
