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

        if (!string.IsNullOrEmpty(request.Id))
            content.Add(new StringContent(request.Id), "Id");

        content.Add(new StringContent(request.DocumentTypeId.ToString()), "DocumentTypeId");
        content.Add(new StringContent(request.WorkflowId.ToString()), "WorkflowId");
        content.Add(new StringContent(request.FormData, Encoding.UTF8), "FormData");

        if (!string.IsNullOrEmpty(request.RowVersion))
            content.Add(new StringContent(request.RowVersion), "RowVersion");

        var response = await _http.PostAsync("Document/SaveWithRowVersion", content);

        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Portal API Error: {result}");

        return result;
    }
}