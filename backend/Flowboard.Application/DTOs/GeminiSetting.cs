namespace Flowboard.API.DTOs
{
    public class GeminiSettings
    {
        public string? ApiKey { get; set; }

        public string Model { get; set; } = "gemini-2.0-flash";

        public string CvPromptPath { get; set; } = "Prompts/cv-extract.txt";

    }
}
