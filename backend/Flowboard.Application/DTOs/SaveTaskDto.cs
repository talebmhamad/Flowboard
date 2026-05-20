namespace Flowboard.Application.DTOs
{
    public class SaveTaskDto
    {
        public string Id { get; set; }
        public string? rowVersion { get; set; }
        public string FormData { get; set; }
    }
}
