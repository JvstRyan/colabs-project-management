using Colabs.ProjectManagement.Application.Contracts;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Mappings.WorkspaceInvitations;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Colabs.ProjectManagement.Domain.Enums;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitations.Commands
{
    public class CreateWorkspaceInvitationCommandHandler : IRequestHandler<CreateWorkspaceInvitationCommand, CreateWorkspaceInvitationCommandResponse>
    {
       
        private readonly IGenericRepository<WorkspaceInvitation> _workspaceInvitationRepository;
        private readonly IGenericRepository<Workspace> _workspaceRepository;
        private readonly ICurrentLoggedInUserService _currentLoggedInUser;
        private readonly IUserRepository _userRepository;

        public CreateWorkspaceInvitationCommandHandler(IGenericRepository<WorkspaceInvitation> workspaceInvitation, ICurrentLoggedInUserService currentLoggedInUser, IUserRepository userRepository,
        IGenericRepository<Workspace> workspaceRepository)
        {
            _workspaceInvitationRepository = workspaceInvitation;
            _currentLoggedInUser = currentLoggedInUser;
            _userRepository = userRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<CreateWorkspaceInvitationCommandResponse> Handle(CreateWorkspaceInvitationCommand request, CancellationToken cancellationToken)
        {
           var response = new CreateWorkspaceInvitationCommandResponse();

           try
           {
                var validator = new CreateWorkspaceInvitationCommandValidator(_userRepository);
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.Message = "Provided details are invalid for workspace invitation creation";
                    response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }
                
                // 1. Check workspace id to see if it exists
                var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId, cancellationToken);

                if (workspace == null)
                {
                    response.Success = false;
                    response.StatusCode = 404;
                    response.Message = "Workspace could not be found";
                    return response;
                }
                
                // 2. Get invitee userId (email check already done in validation section)
                var user = await _userRepository.GetUserByEmailAsync(request.InviteeEmail, cancellationToken);
                Console.WriteLine(user.Email);
                // 3. Currently logged in UserId
                var currentUserId = _currentLoggedInUser.UserId;
                Console.WriteLine(currentUserId);
                
                // 4. Create invitation
                var workspaceInvitation = new WorkspaceInvitation
                {
                    WorkspaceInvitationId = Guid.NewGuid().ToString(),
                    WorkspaceId = request.WorkspaceId,
                    InviterId = currentUserId,
                    InviteeId = user.UserId,
                    Status = InvitationStatus.Pending
                };
                
                await _workspaceInvitationRepository.AddAsync(workspaceInvitation, cancellationToken);
              
                response.Success = true;
                response.Message = "Workspace invitation created successfully";
                return response;
                
           }
           catch (Exception ex)
           {
               response.Success = false;
               response.StatusCode = 500;
               response.Message = $"An unexpected error has occured: {ex.Message}";
               return response;
           }
        }
    }
}
