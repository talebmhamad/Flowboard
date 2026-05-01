using Flowboard.Application.DTOs;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class DocumentService : IDocumentService
{
    private readonly HttpClient _http;
    public DocumentService(HttpClient http)
    {
        _http = http;
    }
    public async Task<string> SaveDocumentAsync(SaveDocumentDto request)
    {
        var content = new MultipartFormDataContent();

        content.Add(new StringContent(request.DocumentTypeId.ToString()), "DocumentTypeId");
        content.Add(new StringContent(request.FormData, Encoding.UTF8), "FormData");

        if (!string.IsNullOrEmpty(request.Id))
            content.Add(new StringContent(request.Id), "Id");

        if (!string.IsNullOrEmpty(request.RowVersion))
            content.Add(new StringContent(request.RowVersion), "RowVersion");

        if (request.WorkflowId.HasValue)
            content.Add(new StringContent(request.WorkflowId.Value.ToString()),"WorkflowId");

        var response = await _http.PostAsync("Document/SaveWithRowVersion", content);

        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Portal API Error: {result}");

        return result;
    }
    public async Task<string> SaveAndSendDocumentAsync(SaveDocumentDto request)
    {
        var content = new MultipartFormDataContent();

        content.Add(new StringContent(request.DocumentTypeId.ToString()), "DocumentTypeId");
        content.Add(new StringContent(request.FormData, Encoding.UTF8), "FormData");

        if (!string.IsNullOrEmpty(request.Id))
            content.Add(new StringContent(request.Id), "Id");

        if (request.WorkflowId.HasValue)
            content.Add(new StringContent(request.WorkflowId.Value.ToString()), "WorkflowId");

        if (!string.IsNullOrEmpty(request.RowVersion))
            content.Add(new StringContent(request.RowVersion), "RowVersion");

        var response = await _http.PostAsync("Document/SaveAndSendWithRowVersion", content);

        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Portal API Error: {result}");

        return result;
    }
    public async Task<string> GetDocumentBasicInfoByTaskId(int taskId)
    {
        var response = await _http.GetAsync($"Document/GetDocumentBasicInfoByTaskId?id={taskId}");

        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Portal API Error: {result}");

        return result;
    }
    public async Task<string> GetDocumentByTaskId(int taskId)
    {
        var response = await _http.GetAsync($"Document/GetDocumentByTaskId?id={taskId}");

        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Portal API Error: {result}");

        return result;
    }
    public async Task<string> GetDocumentById(int id)
    {
        var response = await _http.GetAsync($"Document/Get?id={id}");

        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Portal API Error: {result}");

        return result;
    }
}