using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Features.Workspaces
{
    public class WorkspaceResponse : BaseResponse
    {
        public WorkspaceResponse() : base()
        {
            
        }
        public WorkspaceDto Workspace {get; set;}

    }
}
