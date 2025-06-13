using Colabs.ProjectManagement.Application.Contracts.Infrastructure;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkpsaceProfilePicture;
using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkpspaceProfilePicture;
using MediatR;
using Microsoft.Extensions.Options;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkspaceProfilePicture
{
    public class UploadWorkspaceProfilePictureHandler : IRequestHandler<UploadWorkspaceProfilePictureCommand,
        UploadWorkspaceProfilePictureResponse>
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IBlobStorageSettings _blobStorageSettings;

        public UploadWorkspaceProfilePictureHandler(
            IWorkspaceRepository workspaceRepository,
            IBlobStorageService blobStorageService,
            IBlobStorageSettings blobStorageSettings)
        {
            _workspaceRepository = workspaceRepository;
            _blobStorageService = blobStorageService;
            _blobStorageSettings = blobStorageSettings;
        }


        public async Task<UploadWorkspaceProfilePictureResponse> Handle(UploadWorkspaceProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var response = new UploadWorkspaceProfilePictureResponse();

            try
            {
                var validator = new UploadWorkspaceProfilePictureCommandValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.Message = "Provided details are invalid to upload image to workspace";
                    response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                }
                
                var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId);
                if (workspace == null)
                {
                    response.Success = false;
                    response.Message = "Workspace not found";
                    response.StatusCode = 404;
                    return response;
                }
                
                
                if (!string.IsNullOrEmpty(workspace.ProfileUrl))
                {
                    string oldFileName = Path.GetFileName(new Uri(workspace.ProfileUrl).AbsolutePath);
                    await _blobStorageService.DeleteAsync(oldFileName, _blobStorageSettings.ProfileImageContainer);
                }
                
                
                string fileExtension = Path.GetExtension(request.File.FileName);
                string fileName = $"workspace-{request.WorkspaceId}{fileExtension}";
                
                
                using (var stream = request.File.OpenReadStream())
                {
                    workspace.ProfileUrl = await _blobStorageService.UploadAsync(
                        stream, 
                        fileName, 
                        request.File.ContentType, 
                        _blobStorageSettings.ProfileImageContainer);
                }
                
                await _workspaceRepository.UpdateAsync(workspace);
                
                response.Success = true;
                response.Message = "Profile picture uploaded successfully";
                
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error uploading profile picture: {ex.Message}";
                response.StatusCode = 400;
                return response;
            }

        }
    }
}
