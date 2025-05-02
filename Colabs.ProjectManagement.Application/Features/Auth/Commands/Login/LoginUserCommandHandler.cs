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
            var validator = new ValidateLoginRequest();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new LoginUserCommandResponse
                {
                    Success = false,
                    Message = "Email or password is incorrect",
                    ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            
            var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user == null || !_passwordUtils.VerifyPassword(request.Password, user.PasswordHash))
            {
                return new LoginUserCommandResponse
                {
                    Success = false,
                    Message = "Email or password is incorrect"
                };
            }
            
            var token = _jwtGenerator.CreateToken(user);
            
            return new LoginUserCommandResponse
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                UserId = user.UserId
            };
        }
    }
}
