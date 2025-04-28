namespace Colabs.ProjectManagement.Application.Features.Auth.Queries.GetUserById
{
    public class GetUserByIdResponse
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
