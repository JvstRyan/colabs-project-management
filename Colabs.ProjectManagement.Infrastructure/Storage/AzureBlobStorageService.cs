using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Colabs.ProjectManagement.Application.Contracts.Infrastructure;
using Microsoft.Extensions.Options;

namespace Colabs.ProjectManagement.Infrastructure.Storage
{
    public class AzureBlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobStorageSettings _settings;

        public AzureBlobStorageService(IOptions<BlobStorageSettings> settings)
        {
            _settings = settings.Value;
            _blobServiceClient = new BlobServiceClient(_settings.ConnectionString);
            
        }

        public async Task<string> UploadAsync(Stream content, string fileName, string contentType, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(content, new BlobHttpHeaders {ContentType = contentType});
            
            return blobClient.Uri.ToString();
        }

        public async Task<Stream> DownloadAsync(string fileName, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            
            var download = await blobClient.DownloadAsync();
            return download.Value.Content;
        }

        public async Task DeleteAsync(string fileName, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            
            await blobClient.DeleteIfExistsAsync();
        }

        public string GetBlobUrl(string fileName, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            
            return blobClient.Uri.ToString();
        }
    }
}
