using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Colabs.ProjectManagement.IntegrationTests.Factories;
using FluentAssertions;

namespace Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Infrastructure.Auth
{
    [Collection("Database collection")]
    public abstract class AuthenticationTestBase : IntegrationTestBase
    {
        protected string? AuthToken;
        protected string CurrentUserId;

        protected AuthenticationTestBase(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        protected async Task<(string Token, string UserId)> RegisterUserAsync(string username, string email, string password)
        {
            var registerRequest = new
            {
                Username = username,
                Email = email,
                Password = password,
            };

            var response = await Client.PostAsJsonAsync("/api/users/auth/register", registerRequest);
            
            var content = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Registration failed with status {response.StatusCode}: {content}", 
                    null, 
                    response.StatusCode);

            }

            response.EnsureSuccessStatusCode();
            
            var result = JsonSerializer.Deserialize<AuthResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            result.Should().NotBeNull();
            result!.Token.Should().NotBeNullOrEmpty();
            result.UserId.Should().NotBeEmpty();

            // Set the auth token for subsequent requests
            AuthToken = result.Token;
            CurrentUserId = result.UserId;
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthToken);

            return (result.Token, result.UserId);
        }

        protected async Task<(string Token, string UserId)> LoginUserAsync(string email, string password)
        {
            var loginRequest = new
            {
                Email = email,
                Password = password
            };

            var response = await Client.PostAsJsonAsync("/api/users/auth/login", loginRequest);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            result.Should().NotBeNull();
            result!.Token.Should().NotBeNullOrEmpty();
            result.UserId.Should().NotBeEmpty();

            // Set the auth token for subsequent requests
            AuthToken = result.Token;
            CurrentUserId = result.UserId;
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthToken);

            return (result.Token, result.UserId);
        }

    }
}