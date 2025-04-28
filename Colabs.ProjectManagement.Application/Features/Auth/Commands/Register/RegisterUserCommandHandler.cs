using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Contracts.Utilities;
using Colabs.ProjectManagement.Domain.Entities;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }
        
        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
           var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);

           if (existingUser != null)
           {
               return new RegisterUserCommandResponse
               {
                   Success = false,
                   Message = "User with this email already exists"
               };
           }
           
           var existingUsername = await _userRepository.GetUserByUsernameAsync(request.Username);

           if (existingUsername != null)
           {
               return new RegisterUserCommandResponse
               {
                   Success = false,
                   Message = "User with this username already exists"
               };
           }
           
           var user = new User
           {
               UserId = Guid.NewGuid(),
               Username = request.Username,
               Email = request.Email,
               PasswordHash = _passwordHasher.HashPassword(request.Password)
           };
           
           await _userRepository.AddAsync(user);
           
           var token = _jwtGenerator.CreateToken(user);
           
           return new RegisterUserCommandResponse
           {
               Success = true,
               Message = "User registered successfully",
               Token = token
           };
        }
    }
}
