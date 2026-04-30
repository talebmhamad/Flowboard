using Flowboard.Application.DTOs;
using Flowboard.Application.Interfaces;
using System;
using System.Net.Http;
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

        public async Task<string> GetActiveTasks(TaskRequestDto request)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(request.Draw.ToString()), "draw");
            content.Add(new StringContent(request.Start.ToString()), "start");
            content.Add(new StringContent(request.Length.ToString()), "length");
            content.Add(new StringContent(request.NodeId.ToString()), "nodeId");
            content.Add(new StringContent(request.DocumentTypeId.ToString()), "documentTypeId");
            content.Add(new StringContent(request.StatusId.ToString()), "statusId");

            content.Add(new StringContent(request.ReferenceNumber ?? ""), "referenceNumber");

            content.Add(new StringContent(request.FromDate?.ToString("yyyy-MM-dd") ?? ""), "fromDate");
            content.Add(new StringContent(request.ToDate?.ToString("yyyy-MM-dd") ?? ""), "toDate");

            content.Add(new StringContent(request.Read.ToString().ToLower()), "read");
            content.Add(new StringContent(request.Locked.ToString().ToLower()), "locked");
            content.Add(new StringContent(request.Assigned.ToString().ToLower()), "assigned");
            content.Add(new StringContent(request.Overdue.ToString().ToLower()), "overdue");

            var response = await _http.PostAsync("Task/ListInbox", content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetCompletedTasks(TaskRequestDto request)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(request.Draw.ToString()), "draw");
            content.Add(new StringContent(request.Start.ToString()), "start");
            content.Add(new StringContent(request.Length.ToString()), "length");
            content.Add(new StringContent(request.NodeId.ToString()), "nodeId");
            content.Add(new StringContent(request.DocumentTypeId.ToString()), "documentTypeId");
            content.Add(new StringContent(request.StatusId.ToString()), "statusId");

            content.Add(new StringContent(request.ReferenceNumber ?? ""), "referenceNumber");

            content.Add(new StringContent(request.FromDate?.ToString("yyyy-MM-dd") ?? ""), "fromDate");
            content.Add(new StringContent(request.ToDate?.ToString("yyyy-MM-dd") ?? ""), "toDate");

            var response = await _http.PostAsync("Task/ListCompleted", content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetDraftTasks(TaskRequestDto request)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(request.Draw.ToString()), "draw");
            content.Add(new StringContent(request.Start.ToString()), "start");
            content.Add(new StringContent(request.Length.ToString()), "length");
            content.Add(new StringContent(request.NodeId.ToString()), "nodeId");

            if (request.DocumentTypeId > 0)
                content.Add(new StringContent(request.DocumentTypeId.ToString()), "documentTypeId");

            if (request.FromDate.HasValue)
                content.Add(new StringContent(request.FromDate.Value.ToString("yyyy-MM-dd")), "fromDate");

            if (request.ToDate.HasValue)
                content.Add(new StringContent(request.ToDate.Value.ToString("yyyy-MM-dd")), "toDate");

            var response = await _http.PostAsync("Document/ListDraft", content);

            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Portal API Error: {result}");

            return result;
        }

        public async Task<string> GetTaskDetails(int taskId)
        {
            var response = await _http.GetAsync($"Task/GetTaskDetails?id={taskId}");

            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Portal API Error: {result}");

            return result;
        }

        public async Task<string> SaveTaskAsync(SaveTaskDto request)
        {
            var content = new MultipartFormDataContent();

            if (!string.IsNullOrEmpty(request.Id))
                content.Add(new StringContent(request.Id), "Id");

            if (!string.IsNullOrEmpty(request.RowVersion))
                content.Add(new StringContent(request.RowVersion), "RowVersion");

            content.Add(new StringContent(request.FormData), "FormData");

            var response = await _http.PostAsync("Task/SaveWithRowVersion", content);

            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Portal API Error: {result}");

            return result;
        }

    }
}