

namespace Flowboard.Application.DTOs
{
    public class SaveDocumentDto
    {
        public int DocumentTypeId { get; set; }
        public int? WorkflowId { get; set; }
        public string FormData { get; set; } = string.Empty;
        public string Id { get; set; }
        public string RowVersion { get; set; }
    }
}
