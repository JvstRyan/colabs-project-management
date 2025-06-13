using System.Net.Http.Headers;
using Colabs.ProjectManagement.IntegrationTests.Factories;
using Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Features.Workspaces;
using FluentAssertions;

namespace Colabs.ProjectManagement.IntegrationTests;

public abstract class FileStorageTestBase : WorkspaceTestBase
{
    protected FileStorageTestBase(CustomWebApplicationFactory factory) 
        : base(factory)
    {
    }

    protected async Task<string> UploadWorkspaceImageAsync(string fileName, string contentType)
    {
        // Ensure we have a workspace
        if (CurrentWorkspaceId == string.Empty)
        {
            await CreateWorkspaceAsync($"Test Workspace");
        }
        
        // Create a sample file
        var fileContent = CreateTestFileContent();
        using var content = new MultipartFormDataContent();
        
        var fileContentByteArray = fileContent.ToArray();
        var fileContentStream = new ByteArrayContent(fileContentByteArray);
        fileContentStream.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        
        content.Add(fileContentStream, "file", fileName);
        
        var response = await Client.PostAsync($"/api/workspaces/{CurrentWorkspaceId}/image", content);
        response.EnsureSuccessStatusCode();
        
        var imageUrl = await response.Content.ReadAsStringAsync();
        imageUrl.Should().NotBeNullOrEmpty();
        imageUrl.Should().Contain(fileName);
        
        return imageUrl;
    }

    protected MemoryStream CreateTestFileContent()
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write("This is a test file content");
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}
