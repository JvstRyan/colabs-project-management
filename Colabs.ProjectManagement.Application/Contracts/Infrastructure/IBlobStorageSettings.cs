namespace Colabs.ProjectManagement.Application.Contracts.Infrastructure
{
    public interface IBlobStorageSettings
    {
        string ProfileImageContainer { get; }
        string BannerImageContainer { get; }
        string UserAvatarImageContainer { get; }
    }
}
