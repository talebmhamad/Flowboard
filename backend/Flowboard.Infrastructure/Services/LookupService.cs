// Services/LookupService.cs

using Flowboard.Application.DTOs;
using Flowboard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Flowboard.Application.Services
{
    public class LookupService : ILookupService
    {
        private readonly HttpClient _http;

        public LookupService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<LookupItemDto>> GetLookupItemsByNameAsync(
            string name,
            int language
        )
        {
            var response = await _http.GetAsync(
                $"Lookup/GetLookupItemsByName?name={name}&language={language}"
            );

            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch lookup items");

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<List<LookupItemDto>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return data ?? new List<LookupItemDto>();
        }
    }
}