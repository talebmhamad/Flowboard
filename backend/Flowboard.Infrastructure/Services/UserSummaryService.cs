using Flowboard.Application.DTOs;
using Flowboard.Application.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<UserSummaryDto> GetSummary(string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));

            return new UserSummaryDto
            {
                Draft = await GetCount("Task/GetDraftCounts?nodeId=1"),
                Inbox = await GetCount("Task/GetInboxCounts?nodeId=2"),
                Completed = await GetCount("Task/GetCompletedCounts?nodeId=3"),
                MyRequests = await GetCount("Task/GetMyRequestsCounts?nodeId=4"),
                Closed = await GetCount("Task/GetClosedCounts?nodeId=6")
            };
        }

        private async Task<int> GetCount(string url)
        {
            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return 0;

            var result = await response.Content.ReadAsStringAsync();

            return int.TryParse(result, out int value) ? value : 0;
        }
    }
}