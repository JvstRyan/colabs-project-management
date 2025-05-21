namespace Colabs.ProjectManagement.Application.Contracts.Infrastructure
{
    public interface IBlobStorageService
    {
        Task<string> UploadAsync(Stream content, string fileName, string contentType, string containerName);
        Task<Stream> DownloadAsync(string fileName, string containerName);
        Task DeleteAsync(string fileName, string containerName);
        string GetBlobUrl(string fileName, string containerName);
    }
}
