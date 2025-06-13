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
            var response = new UploadWorkspaceBannerResponse();
            try
            {
                var validator = new UploadWorkspaceBannerCommandValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.Message = "Provided details are invalid to upload banner to workspace";
                    response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }
                
                // 1. Get workspace
                var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId, cancellationToken);

                if (workspace == null)
                {
                    response.Success = false;
                    response.StatusCode = 404;
                    response.Message = "Workspace not found";
                    return response;
                }
            
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
            
                // 6. Return response
                response.Success = true;
                response.Message = "Banner uploaded successfully";
                return response;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = $"Banner could not be uploaded: {ex.Message}";
                return response;
            }
            
        }
    }
}
