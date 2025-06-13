using Colabs.ProjectManagement.Application.Contracts.Infrastructure;
using Colabs.ProjectManagement.Infrastructure.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO;
using System.Text;
using Colabs.ProjectManagement.IntegrationTests.Factories;

namespace Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage;

public abstract class AzureBlobStorageTestBase : IClassFixture<CustomWebApplicationFactory>
{
    protected readonly IBlobStorageService BlobStorageService;
    protected readonly CustomWebApplicationFactory Factory;

    protected AzureBlobStorageTestBase(CustomWebApplicationFactory factory)
    {
        Factory = factory;

        // Create a new service collection for configuring the blob storage service
        var serviceProvider = new ServiceCollection()
            .Configure<BlobStorageSettings>(options => 
            {
                options.ConnectionString = 
                    "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;";
            })
            .BuildServiceProvider();

        var settings = serviceProvider.GetRequiredService<IOptions<BlobStorageSettings>>();
        BlobStorageService = new AzureBlobStorageService(settings);
    }

    protected Stream CreateTestImageStream()
    {
        // Create a simple test image or load one from resources
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write("This is a test file content");
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}
