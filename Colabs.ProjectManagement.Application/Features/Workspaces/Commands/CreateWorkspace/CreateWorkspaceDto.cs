namespace Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace
{
    public class CreateWorkspaceDto
    {
        public string WorkspaceId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
