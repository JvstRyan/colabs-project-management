using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Auth.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<GetUserByIdResponse>
    {
        public string UserId { get; set; }
    }
}
