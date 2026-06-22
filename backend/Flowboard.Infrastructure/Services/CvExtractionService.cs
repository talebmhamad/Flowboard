using Flowboard.API.DTOs;
using Flowboard.Application.Interfaces.Flowboard.Application.Interfaces;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Flowboard.Infrastructure.Services
{
    public class CvExtractionService : ICvExtractionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model;
        private readonly string _promptText;

        public CvExtractionService(IHttpClientFactory httpClientFactory,IOptions<GeminiSettings> geminiSettings)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(60);

            _apiKey = geminiSettings.Value.ApiKey!;
            _model = geminiSettings.Value.Model;

            var promptPath = Path.Combine(
                AppContext.BaseDirectory,
                geminiSettings.Value.CvPromptPath
            );

            if (!File.Exists(promptPath))
                throw new FileNotFoundException($"Prompt file not found: {promptPath}");

            _promptText = File.ReadAllText(promptPath);
        }

        public async Task<object> ExtractCvAsync(string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
                throw new Exception("FileUrl is required");

            var base64 = fileUrl.Contains(",")
                ? fileUrl.Split(",")[1]
                : fileUrl;

            var result = await CallGeminiVision(base64);

            return result;
        }
        private async Task<string> CallGeminiVision(string base64)
        {
            var body = new
            {
                contents = new[]
                {
            new
            {
                parts = new object[]
                {
                    new
                    {
                        inlineData = new
                        {
                            mimeType = "application/pdf",
                            data = base64
                        }
                    },
                    new
                    {
                        text = _promptText
                    }
                }
            }
        }
            };

            var json = JsonSerializer.Serialize(body);

            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"https://generativelanguage.googleapis.com/v1beta/models/{_model}:generateContent?key={_apiKey}",
                content
            );

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Gemini CV Error: {responseContent}");

            using var doc = JsonDocument.Parse(responseContent);

            return doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString()!;
        }
    }
}