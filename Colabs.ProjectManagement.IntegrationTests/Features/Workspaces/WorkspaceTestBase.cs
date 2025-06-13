using System.Net.Http.Json;
using System.Text.Json;
using Colabs.ProjectManagement.IntegrationTests.Factories;
using Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Infrastructure.Auth;
using FluentAssertions;

namespace Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Features.Workspaces
{
    [Collection("Database collection")]
    public abstract class WorkspaceTestBase : AuthenticationTestBase
    {
        protected string CurrentWorkspaceId;

        protected WorkspaceTestBase(CustomWebApplicationFactory factory)
            : base(factory)
        {
        }

        protected async Task<string> CreateWorkspaceAsync(string name)
        {
            var randomSuffix = Guid.NewGuid().ToString()[..8];
            
            // Ensure we're authenticated
            if (string.IsNullOrEmpty(AuthToken))
            {
                await RegisterUserAsync($"TestUser_{randomSuffix}", $"user_{randomSuffix}@example.com", "Password123!");
            }

            var createWorkspaceRequest = new
            {
                Name = name,
            };

            var response = await Client.PostAsJsonAsync("/api/workspaces", createWorkspaceRequest);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CreateWorkspaceResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            result.Should().NotBeNull();
            result!.Workspace.WorkspaceId.Should().NotBeEmpty();
            result.Workspace.Name.Should().Be(name);

            // Store the workspace ID for subsequent requests
            CurrentWorkspaceId = result.Workspace.WorkspaceId;

            return result.Workspace.WorkspaceId;
        }

        protected async Task<List<GetAllWorkspace>> GetWorkspacesAsync()
        {
            var randomSuffix = Guid.NewGuid().ToString()[..8];
            
            // Ensure we're authenticated
            if (string.IsNullOrEmpty(AuthToken))
            {
                await RegisterUserAsync($"TestUser_{randomSuffix}", $"user_{randomSuffix}@example.com", "Password123!");
            }

            var response = await Client.GetAsync("/api/workspaces");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            
            var result = JsonSerializer.Deserialize<GetAllWorkspaceResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            result.Should().NotBeNull();

            return result?.Workspaces;

        }
    }
}
