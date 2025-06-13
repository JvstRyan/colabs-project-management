using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Colabs.ProjectManagement.IntegrationTests.Factories;
using Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage;

namespace Colabs.ProjectManagement.IntegrationTests.Storage;

public class AzureBlobStorageServiceTests : AzureBlobStorageTestBase
{
    public AzureBlobStorageServiceTests(CustomWebApplicationFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task UploadAsync_ShouldUploadFileToContainer_AndReturnUrl()
    {
        // Arrange
        var containerName = "test-container";
        var fileName = "test-file.txt";
        var contentType = "text/plain";
        var content = CreateTestImageStream();

        // Act
        var result = await BlobStorageService.UploadAsync(content, fileName, contentType, containerName);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(containerName);
        result.Should().Contain(fileName);
    }

    [Fact]
    public async Task UploadAndDownloadAsync_ShouldRetrieveUploadedFile()
    {
        // Arrange
        var containerName = "test-container";
        var fileName = "test-download-file.txt";
        var contentType = "text/plain";
        var testContent = "This is test content";
        
        var contentStream = new MemoryStream();
        var writer = new StreamWriter(contentStream);
        await writer.WriteAsync(testContent);
        await writer.FlushAsync();
        contentStream.Position = 0;

        // Act
        await BlobStorageService.UploadAsync(contentStream, fileName, contentType, containerName);
        var downloadedStream = await BlobStorageService.DownloadAsync(fileName, containerName);
        
        // Assert
        using var reader = new StreamReader(downloadedStream);
        var downloadedContent = await reader.ReadToEndAsync();
        downloadedContent.Should().Be(testContent);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveFileFromContainer()
    {
        // Arrange
        var containerName = "test-container";
        var fileName = "test-delete-file.txt";
        var contentType = "text/plain";
        var content = CreateTestImageStream();
        
        await BlobStorageService.UploadAsync(content, fileName, contentType, containerName);
        
        // Act
        await BlobStorageService.DeleteAsync(fileName, containerName);
        
        // Assert
        // Attempting to download the deleted file should throw an exception
        await Assert.ThrowsAsync<Azure.RequestFailedException>(async () => 
            await BlobStorageService.DownloadAsync(fileName, containerName));
    }
}