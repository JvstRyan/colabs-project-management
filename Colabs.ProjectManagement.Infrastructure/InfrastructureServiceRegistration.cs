using Colabs.ProjectManagement.Application.Contracts.Utilities;
using Colabs.ProjectManagement.Infrastructure.Hashing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Colabs.ProjectManagement.Application.Contracts.Infrastructure;
using Colabs.ProjectManagement.Infrastructure.Storage;
using Colabs.ProjectManagement.Infrastructure.Tokens;
using Microsoft.Extensions.Options;


namespace Colabs.ProjectManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
            
            services.AddSingleton<IPasswordUtils, PasswordHash>();
            services.AddScoped<IJwtGenerator, GenerateToken>();
            services.AddSingleton<IBlobStorageService, AzureBlobStorageService>();
            
            services.AddSingleton<IBlobStorageSettings>(sp => 
                sp.GetRequiredService<IOptions<BlobStorageSettings>>().Value);

            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        configuration["JwtSettings:Key"] ?? throw new InvalidOperationException(
                            "JWT Key is not configured"))),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            
            return services;
        }
    }
}
