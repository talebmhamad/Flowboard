using System.Net.Http;
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

        public async Task<string> GetActiveTasks()
        {
            var response = await _http.PostAsync("Task/ListInbox",
                new StringContent("{}", Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetCompletedTasks()
        {
            var response = await _http.PostAsync("Task/ListCompleted",
                new StringContent("{}", Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetDraftTasks()
        {
            var response = await _http.PostAsync("Document/ListDraft",
                new StringContent("{}", Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetMyRequests()
        {
            var response = await _http.PostAsync("Document/ListMyRequests",
                new StringContent("{}", Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }
    }
}