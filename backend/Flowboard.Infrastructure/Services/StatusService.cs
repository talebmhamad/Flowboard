using Flowboard.Application.DTOs;
using Flowboard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class StatusService : IStatusService
{
    private readonly HttpClient _http;

    public StatusService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<StatusDto>> GetAllAsync()
    {
        var response = await _http.GetAsync("Status/ListStatuses");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to fetch statuses");

        var json = await response.Content.ReadAsStringAsync();

        var data = JsonSerializer.Deserialize<List<StatusDto>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return data ?? new List<StatusDto>();
    }
}