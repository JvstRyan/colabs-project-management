using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace
{
    public class CreateWorkspaceCommandResponse : BaseResponse
    {
        public CreateWorkspaceCommandResponse() : base()
        {
            
        }
        public CreateWorkspaceDto Workspace {get; set;} = default;
    }
    
}
