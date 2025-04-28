using Colabs.ProjectManagement.Application.Features.Auth.Queries.GetUserById;
using Colabs.ProjectManagement.Domain.Entities;

namespace Colabs.ProjectManagement.Application.Mappings
{
    public static class UserMapper
    {
        public static GetUserByIdResponse ToGetUserByIdResponse(this User user)
        {
            return new GetUserByIdResponse()
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl
            };
        }
    }
}
