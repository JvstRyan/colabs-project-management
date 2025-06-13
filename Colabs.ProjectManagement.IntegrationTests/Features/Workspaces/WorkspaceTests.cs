using Colabs.ProjectManagement.IntegrationTests.Factories;
using FluentAssertions;

namespace Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Features.Workspaces
{
    [Collection("Database collection")]
    public class WorkspaceTests : WorkspaceTestBase
    {
        public WorkspaceTests(CustomWebApplicationFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task CreateWorkspace_ShouldCreateWorkspace_AndReturnDetails()
        {
            // Arrange
            var uniqueSuffix = Guid.NewGuid().ToString()[..8];
            var username = $"WorkspaceTestUser_{uniqueSuffix}";
            var email = $"workspace_test_{uniqueSuffix}@example.com";
            var password = "Password123!";

            await RegisterUserAsync(username, email, password);

            var workspaceName = $"Test Workspace";

            // Act
            var workspaceId = await CreateWorkspaceAsync(workspaceName);

            // Assert
            workspaceId.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetWorkspaces_ShouldReturnAllUserWorkspaces()
        {
            // Arrange
            var uniqueSuffix = Guid.NewGuid().ToString()[..8];
            var username = $"WorkspaceTestUser_{uniqueSuffix}";
            var email = $"workspace_test_{uniqueSuffix}@example.com";
            var password = "Password123!";

            await RegisterUserAsync(username, email, password);

            var workspaceName1 = $"Test Workspace 1";
            var workspaceName2 = $"Test Workspace 2";

            await CreateWorkspaceAsync(workspaceName1);
            await CreateWorkspaceAsync(workspaceName2);

            // Act
            var workspaces = await GetWorkspacesAsync();

            // Assert
            workspaces.Should().HaveCountGreaterOrEqualTo(2);
        }
    }
}
