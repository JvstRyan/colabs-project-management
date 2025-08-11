using Colabs.ProjectManagement.Application.Contracts.Infrastructure;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkspaceBanner
{
    public class UploadWorkspaceBannerCommandHandler : IRequestHandler<UploadWorkspaceBannerCommand, UploadWorkspaceBannerResponse>
    {
        private readonly IGenericRepository<Workspace> _workspaceRepository;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IBlobStorageSettings _blobStorageSettings;

        public UploadWorkspaceBannerCommandHandler(IGenericRepository<Workspace> workspaceRepository, IBlobStorageService blobStorageService, IBlobStorageSettings blobStorageSettings)
        {
            _workspaceRepository = workspaceRepository;
            _blobStorageService = blobStorageService;
            _blobStorageSettings = blobStorageSettings;
        }

        public async Task<UploadWorkspaceBannerResponse> Handle(UploadWorkspaceBannerCommand request, CancellationToken cancellationToken)
        {
            // 1. Get workspace
            var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId, cancellationToken);

            if (workspace is null)
                throw new NotFoundException("Workspace", request.WorkspaceId);

            // 2. Remove current banner
            if (!string.IsNullOrEmpty(workspace.BannerUrl))
            {
                string oldFileName = Path.GetFileName(new Uri(workspace.BannerUrl).AbsolutePath);
                await _blobStorageService.DeleteAsync(oldFileName, _blobStorageSettings.BannerImageContainer);
            }

            // 3. File naming
            string fileExtension = Path.GetExtension(request.File.FileName);
            string fileName = $"workspace-{request.WorkspaceId}{fileExtension}";

            // 4. Uploading banner
            using (var stream = request.File.OpenReadStream())
            {
                workspace.BannerUrl = await _blobStorageService.UploadAsync(
                    stream,
                    fileName,
                    request.File.ContentType,
                    _blobStorageSettings.BannerImageContainer);
            }

            // 5. Update workspace
            await _workspaceRepository.UpdateAsync(workspace, cancellationToken);

            return new UploadWorkspaceBannerResponse(true);
        }
    }
}