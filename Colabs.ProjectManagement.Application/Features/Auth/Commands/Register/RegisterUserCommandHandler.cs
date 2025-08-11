using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Contracts.Utilities;
using Colabs.ProjectManagement.Domain.Entities;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordUtils _passwordUtils;
        private readonly IJwtGenerator _jwtGenerator;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordUtils passwordUtils, IJwtGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _passwordUtils = passwordUtils;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid().ToString();
            var user = new User
            {
                UserId = userId,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _passwordUtils.HashPassword(request.Password)
            };

            await _userRepository.AddAsync(user, cancellationToken);
            var token = _jwtGenerator.CreateToken(user);

            return new RegisterUserCommandResponse(token, userId);
        }
    }
}