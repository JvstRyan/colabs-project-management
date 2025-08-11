using Colabs.ProjectManagement.Application.Contracts.Infrastructure;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkpsaceProfilePicture;
using MediatR;

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
            var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId);

            if (workspace is null)
                throw new NotFoundException("Workspace", request.WorkspaceId);

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

            return new UploadWorkspaceProfilePictureResponse(true);
        }
    }
}