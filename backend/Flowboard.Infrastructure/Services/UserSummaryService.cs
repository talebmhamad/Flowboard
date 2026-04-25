using Flowboard.Application.DTOs;
using Flowboard.Application.Interfaces;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Flowboard.Infrastructure.Services
{
    public class UserSummaryService : IUserSummaryService
    {
        private readonly HttpClient _http;

        public UserSummaryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<UserSummaryDto> GetSummary()
        {
            return new UserSummaryDto
            {
                Draft = await GetCount("Document/GetDraftCounts?nodeId=1"),
                Inbox = await GetCount("Task/GetInboxCounts?nodeId=2"),
                Completed = await GetCount("Task/GetCompletedCounts?nodeId=3"),
                MyRequests = await GetCount("Document/GetMyRequestsCounts?nodeId=4"),
                Closed = await GetCount("Document/GetClosedCounts?nodeId=6")
            };
        }

        private async Task<CountDto> GetCount(string url)
        {
            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new CountDto();

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<CountDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return data ?? new CountDto();
        }

    }
}