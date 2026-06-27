using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using ToDoApp.Application.Interfaces;

namespace ToDoApp.Infrastructure.Services;

public class ExternalAuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ExternalAuthService> _logger;
    private readonly string _authApiUrl;

    public ExternalAuthService(HttpClient httpClient, IConfiguration config, ILogger<ExternalAuthService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _authApiUrl = config["AuthMicroserviceUrl"] ?? throw new ArgumentNullException("AuthMicroserviceUrl");
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        try
        {
            var payload = new { Username = username, Password = password };
            
            var response = await _httpClient.PostAsJsonAsync($"{_authApiUrl}/api/auth/login", payload);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return result?.Token;
            }

            _logger.LogWarning("Auth API returned status {StatusCode} for user {Username}", response.StatusCode, username);
            return null;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to connect to Auth Microservice. Is it running?");
            return null;
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}
