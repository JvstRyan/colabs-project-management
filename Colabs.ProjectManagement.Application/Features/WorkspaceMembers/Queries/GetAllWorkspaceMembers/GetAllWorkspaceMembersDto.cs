namespace Colabs.ProjectManagement.Application.Features.WorkspaceMembers.Queries.GetAllWorkspaceMembers
{
    public class GetAllWorkspaceMembersDto
    {
        public string WorkspaceMemberUsername {get; set;} = string.Empty;
        public string WorkspaceMemberEmail {get; set;} = string.Empty;
        public string WorkspaceMemberRole { get; set; } = string.Empty;
        public string? WorkspaceMemberAvatarUrl {get; set;}
    }
}
