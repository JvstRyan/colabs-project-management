﻿namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
