using System;


namespace Flowboard.Application.DTOs
{
    public class TaskInboxRequestDto
    {
        // DataTables pagination
        public int Draw { get; set; } = 1;
        public int Start { get; set; } = 0;
        public int Length { get; set; } = 10;

        // Filters
        public int NodeId { get; set; }
        public int DocumentTypeId { get; set; }
        public int StatusId { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public bool Read { get; set; }
        public bool Locked { get; set; }
        public bool Assigned { get; set; }
        public bool Overdue { get; set; }
    }
}
