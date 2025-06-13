using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Features.Workspaces
{
    public class CreateWorkspaceResponse : BaseResponse
    {
        public CreateWorkspaceResponse() : base()
        {
            
        }
        public WorkspaceDto Workspace {get; set;}

    }
}
