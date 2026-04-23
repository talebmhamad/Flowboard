using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Flowboard.Application.Interfaces;

namespace Flowboard.Infrastructure.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly HttpClient _http;

        public UserTaskService(HttpClient http)
        {
            _http = http;
        }

        private void SetToken(string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        }

        public async Task<object> GetActiveTasks(string token)
        {
            SetToken(token);

            var response = await _http.PostAsync("Task/ListInbox",
                new StringContent("{}", Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<object> GetCompletedTasks(string token)
        {
            SetToken(token);

            var response = await _http.PostAsync("Task/ListCompleted",
                new StringContent("{}", Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<object> GetDraftTasks(string token)
        {
            SetToken(token);

            var response = await _http.PostAsync("Document/ListDraft",
                new StringContent("{}", Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<object> GetMyRequests(string token)
        {
            SetToken(token);

            var response = await _http.PostAsync("Document/ListMyRequests",
                new StringContent("{}", Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }
    }
}