using System;


namespace Flowboard.Application.DTOs
{
    public class InboxTaskDto
    {
        public long Id { get; set; }

        public long DocumentId { get; set; }

        public int? DocumentTypeId { get; set; }

        public string DocumentType { get; set; }

        public string ReferenceNumber { get; set; }

        public long? OwnerUserId { get; set; }

        public string OwnerUserName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? OpenedDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public int? StatusId { get; set; }

        public string StatusName { get; set; }

        public bool? IsAssigned { get; set; }

        public bool? IsTransferred { get; set; }

        public bool? IsRead { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsOverdue { get; set; }

        public string Instruction { get; set; }

        public string ReplyInstruction { get; set; }
    }
}
