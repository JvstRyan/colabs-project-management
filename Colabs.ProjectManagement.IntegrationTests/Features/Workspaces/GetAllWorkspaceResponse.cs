using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Features.Workspaces
{
    public class GetAllWorkspaceResponse : BaseResponse
    {
        public GetAllWorkspaceResponse() : base()
        {
            
        }
        public List<GetAllWorkspace> Workspaces { get; set; } = new();
    }
}
