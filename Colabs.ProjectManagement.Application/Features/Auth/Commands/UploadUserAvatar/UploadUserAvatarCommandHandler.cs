using Colabs.ProjectManagement.Application.Contracts.Infrastructure;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Domain.Entities;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.UploadUserAvatar
{
    public class UploadUserAvatarCommandHandler : IRequestHandler<UploadUserAvatarCommand, UploadUserAvatarCommandResponse>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IBlobStorageSettings _blobStorageSettings;

        public UploadUserAvatarCommandHandler(IGenericRepository<User> userRepository,
            IBlobStorageService blobStorageService,
            IBlobStorageSettings blobStorageSettings)
        {
            _userRepository = userRepository;
            _blobStorageService = blobStorageService;
            _blobStorageSettings = blobStorageSettings;
        }
     

        public async Task<UploadUserAvatarCommandResponse> Handle(UploadUserAvatarCommand request, CancellationToken cancellationToken)
        {
            var response = new UploadUserAvatarCommandResponse();
            
            try
            {
                var validator = new UploadUserAvatarCommandValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.Message = "Provided details are invalid to upload image to user profile";
                    response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }
                
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

                if (user == null)
                {
                    response.Success = false;
                    response.StatusCode = 404;
                    response.Message = "User could not be found";
                    return response;
                }
                
                if (!string.IsNullOrEmpty(user.AvatarUrl))
                {
                    string oldFileName = Path.GetFileName(new Uri(user.AvatarUrl).AbsolutePath);
                    await _blobStorageService.DeleteAsync(oldFileName, _blobStorageSettings.BannerImageContainer);
                }
                
                string fileExtension = Path.GetExtension(request.File.FileName);
                string fileName = $"user-{request.UserId}{fileExtension}";
                
                using (var stream = request.File.OpenReadStream())
                {
                    user.AvatarUrl = await _blobStorageService.UploadAsync(
                        stream, 
                        fileName, 
                        request.File.ContentType, 
                        _blobStorageSettings.UserAvatarImageContainer);
                }
                
                await _userRepository.UpdateAsync(user, cancellationToken);
                
                response.Success = true;
                response.Message = "User avatar uploaded successfully";
                return response;
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = $"User avatar could not be uploaded: {ex.Message}";
                return response;
            }
        }
    }
}
