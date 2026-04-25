using Flowboard.Application.DTOs;
using Flowboard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Flowboard.Infrastructure.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly HttpClient _http;

        public WorkflowService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<WorkflowDto>> GetWorkflowsAsync()
        {
            var url = "DocumentType/ListDocumentTypes?delegationId=null";

            var response = await _http.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            // 🔥 Debug (remove later)
            Console.WriteLine("Workflow Response:");
            Console.WriteLine(content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Portal API Error: {response.StatusCode} - {content}");

            if (string.IsNullOrWhiteSpace(content))
                return new List<WorkflowDto>();

            var data = JsonSerializer.Deserialize<List<WorkflowDto>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return data ?? new List<WorkflowDto>();
        }
    }

}