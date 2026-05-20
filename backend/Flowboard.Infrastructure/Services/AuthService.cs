using Flowboard.Application.Interfaces;
using Flowboard.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Flowboard.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IamSettings _iam;

        public AuthService(HttpClient httpClient, IOptions<IamSettings> iamOptions)
        {
            _httpClient = httpClient;
            _iam = iamOptions.Value;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _iam.ClientId),
                new KeyValuePair<string, string>("client_secret", _iam.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("scope", "IdentityServerApi"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            var url = _iam.Url.TrimEnd('/') + "/connect/token";

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new UnauthorizedAccessException("Login failed: " + error);
            }

            var json = await response.Content.ReadAsStringAsync();

            var doc = JsonDocument.Parse(json);

            try
            {
                JsonElement tokenElement;

                if (!doc.RootElement.TryGetProperty("access_token", out tokenElement))
                    throw new Exception("Token not found in response");

                var token = tokenElement.GetString();

                if (string.IsNullOrEmpty(token))
                    throw new Exception("Token is empty");

                return token;
            }
            finally
            {
                doc.Dispose();
            }
        }
    }
}