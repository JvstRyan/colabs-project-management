using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Contracts.Utilities;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
    {
        private readonly IPasswordUtils _passwordUtils;
        private readonly IUserRepository _userRepository;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginUserCommandHandler(IPasswordUtils passwordUtils, IUserRepository userRepository, IJwtGenerator jwtGenerator)
        {
            _passwordUtils = passwordUtils;
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user is null)
                throw new NotFoundException("User not found");

            if (!_passwordUtils.VerifyPassword(request.Password, user.PasswordHash))
                throw new BadRequestException("Password does not match");

            var token = _jwtGenerator.CreateToken(user);

            return new LoginUserCommandResponse(token, user.UserId);
        }
    }
}