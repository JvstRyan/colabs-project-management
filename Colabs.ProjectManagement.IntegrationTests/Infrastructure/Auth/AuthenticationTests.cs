using Colabs.ProjectManagement.IntegrationTests.Factories;
using Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Infrastructure.Auth;
using FluentAssertions;

namespace Colabs.ProjectManagement.IntegrationTests.Authentication;

[Collection("Database collection")]
public class AuthenticationTests : AuthenticationTestBase
{
    public AuthenticationTests(CustomWebApplicationFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task Register_ShouldCreateUser_AndReturnToken()
    {
        var randomSuffix = Guid.NewGuid().ToString()[..8];
        
        // Arrange
        var username = $"TestUser_{randomSuffix}";
        var email = $"user_{randomSuffix}@example.com";
        var password = "Password123!";
        
        // Act
        var (token, userId) = await RegisterUserAsync(username, email, password);
        
        // Assert
        token.Should().NotBeNullOrEmpty();
        userId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task Login_ShouldReturnToken_ForRegisteredUser()
    {
        
        var randomSuffix = Guid.NewGuid().ToString()[..8];
        
        // Arrange
        var username = $"TestUser_{randomSuffix}";
        var email = $"user_{randomSuffix}@example.com";
        var password = "Password123!";
        await RegisterUserAsync(username, email, password);
        
        // Clear the auth token to test login
        AuthToken = null;
        Client.DefaultRequestHeaders.Authorization = null;
        
        // Act
        var (token, userId) = await LoginUserAsync(email, password);
        
        // Assert
        token.Should().NotBeNullOrEmpty();
        userId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task Login_ShouldFail_ForNonExistentUser()
    {
        // Arrange
        var email = $"nonexistent123@example.com";
        var password = "Password123!";
        
        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(
            async () => await LoginUserAsync(email, password));
    }
}
