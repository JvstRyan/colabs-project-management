using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.Sprints.Queries
{
    public class GetAllSprintsQueryResponse : BaseResponse
    {
        public GetAllSprintsQueryResponse() : base()
        {
            
        }
        
        public IReadOnlyList<GetAllSprintsQueryDto> Sprints {get; set;}
    }
}
