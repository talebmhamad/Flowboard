namespace Flowboard.Application.DTOs.Document
{

    public class DocumentDetailsDto
    {
        public long Id { get; set; }

        public long? DocumentTypeId { get; set; }

        public string DocumentTypeName { get; set; }

        public string FormData { get; set; }

        public string FormDesigner { get; set; }

        public string FormDesignerTranslation { get; set; }

        public bool IsEnableEdit { get; set; }

        public string RowVersion { get; set; }

        public int? Status { get; set; }
    }
}
