using Colabs.ProjectManagement.Application.Contracts.Infrastructure;

namespace Colabs.ProjectManagement.Infrastructure.Storage
{
    public class BlobStorageSettings : IBlobStorageSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string ProfileImageContainer {get; set;} = "profile-images";
        public string BannerImageContainer {get; set;} = "banner-images";
        public string UserAvatarImageContainer { get; set; } = "user-avatars";
        public string TaskFileContainer { get; set; } = "task-files";
    }
}
