namespace Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetWorkspace
{
    public class GetWorkspaceQueryDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;
        public string? ProfileUrl {get; set;} = string.Empty;
        public string? BannerUrl { get; set; } = string.Empty;
    }
}
