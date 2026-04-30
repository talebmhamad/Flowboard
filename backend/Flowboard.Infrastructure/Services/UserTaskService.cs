using Flowboard.Application.DTOs;
using Flowboard.Application.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Flowboard.Infrastructure.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly HttpClient _http;

        public UserTaskService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GetActiveTasks(TaskInboxRequestDto request)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(request.Draw.ToString()), "draw");
            content.Add(new StringContent(request.Start.ToString()), "start");
            content.Add(new StringContent(request.Length.ToString()), "length");
            content.Add(new StringContent(request.NodeId.ToString()), "nodeId");
            content.Add(new StringContent(request.DocumentTypeId.ToString()), "documentTypeId");
            content.Add(new StringContent(request.StatusId.ToString()), "statusId");

            content.Add(new StringContent(request.ReferenceNumber ?? ""), "referenceNumber");

            content.Add(new StringContent(request.FromDate?.ToString("dd/MM/yyyy") ?? ""), "fromDate");
            content.Add(new StringContent(request.ToDate?.ToString("dd/MM/yyyy") ?? ""), "toDate");

            content.Add(new StringContent(request.Read.ToString().ToLower()), "read");
            content.Add(new StringContent(request.Locked.ToString().ToLower()), "locked");
            content.Add(new StringContent(request.Assigned.ToString().ToLower()), "assigned");
            content.Add(new StringContent(request.Overdue.ToString().ToLower()), "overdue");

            var response = await _http.PostAsync("Task/ListInbox", content);

            response.EnsureSuccessStatusCode();

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