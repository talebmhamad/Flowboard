using Flowboard.Application.DTOs;
using Flowboard.Application.Interfaces;
using System.Text.Json;

namespace Flowboard.Application.Services
{
    public class LookupService : ILookupService
    {
        private readonly HttpClient _http;

        public LookupService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<LookupItemDto>> GetLookupItemsByNameAsync(string name,int language)
        {
            var response = await _http.GetAsync(
                $"Lookup/GetLookupItemsByName?name={name}&language={language}"
            );

            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch lookup items");

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<List<LookupItemDto>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return data ?? new List<LookupItemDto>();
        }
        public async Task<JsonElement> SearchUsersAsync(string text,bool showOnlyActiveUsers,int? language)
        {
            var url =
                $"Api/SearchUsers?text={Uri.EscapeDataString(text ?? "")}" +
                $"&showOnlyActiveUsers={showOnlyActiveUsers.ToString().ToLower()}";

            if (language.HasValue)
                url += $"&language={language.Value}";

            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to search users");

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<JsonElement>(json);
        }
    }
}