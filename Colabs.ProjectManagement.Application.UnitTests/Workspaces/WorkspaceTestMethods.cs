using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;

namespace Colabs.ProjectManagement.Application.UnitTests.Workspaces.Mocks
{
    public static class WorkspaceTestMethods
    {
        public static Workspace CreateTestWorkspace(string ownerId = "test-user-id")
        {
            return new Workspace
            {
                WorkspaceId = Guid.NewGuid().ToString(),
                Name = "Test Workspace",
                Description = "Test Description",
                OwnerId = ownerId,
                ProfileUrl = "https://test.com/profile.jpg",
                BannerUrl = "https://test.com/banner.jpg"
            };
        }

        public static CreateWorkspaceCommand CreateValidCommand()
        {
            return new CreateWorkspaceCommand
            {
                Name = "Test Workspace",
                Description = "Test Description",
                ProfileUrl = "https://test.com/profile.jpg",
                BannerUrl = "https://test.com/banner.jpg"
            };

        }
        
        public static CreateWorkspaceCommand CreateInvalidCommand()
        {
            return new CreateWorkspaceCommand
            {
                Name = "", 
                Description = "Test description"
            };

        }
    }
}
