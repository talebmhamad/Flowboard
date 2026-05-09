using Flowboard.Application.DTOs;
using Intalio.Case.Portal.Core.DAL;
using Microsoft.EntityFrameworkCore;

namespace Flowboard.Intalio.Repositories
{
    public class TaskRepository
    {
        private readonly CasePortalContext _db;

        public TaskRepository(CasePortalContext db)
        {
            _db = db;
        }

        public async Task<DataTableResponse<InboxTaskDto>> GetInboxTasksAsync(List<long> userIds,TaskRequestDto request)
        {
            var query =
                from task in _db.Task

                join user in _db.Users
                    on task.UserId equals user.Id into userJoin
                from user in userJoin.DefaultIfEmpty()

                join document in _db.Document
                    on task.DocumentId equals document.Id into documentJoin
                from document in documentJoin.DefaultIfEmpty()

                join documentType in _db.DocumentTypesBase
                    on document.DocumentTypeBaseId equals documentType.Id into documentTypeJoin
                from documentType in documentTypeJoin.DefaultIfEmpty()

                join status in _db.Status
                    on task.StatusId equals status.Id into statusJoin
                from status in statusJoin.DefaultIfEmpty()

                where
                    // Only inbox active tasks
                    task.ClosedDate == null &&

                    // Assigned users
                    task.UserId.HasValue &&
                    userIds.Contains(task.UserId!.Value) &&

                    // Reference number filter
                    (
                        string.IsNullOrEmpty(request.ReferenceNumber)
                        ||
                        document.ReferenceNumber.Contains(request.ReferenceNumber)
                    ) &&

                    // Document type filter
                    (
                        request.DocumentTypeId == 0
                        ||
                        document.DocumentTypeBaseId == request.DocumentTypeId
                    ) &&

                    // Status filter
                    (
                        request.StatusId == 0
                        ||
                        task.StatusId == request.StatusId
                    ) &&

                    // From date filter
                    (
                        !request.FromDate.HasValue
                        ||
                        task.CreatedDate >= request.FromDate
                    ) &&

                    // To date filter
                    (
                        !request.ToDate.HasValue
                        ||
                        task.CreatedDate <= request.ToDate
                    ) &&

                    // Assigned filter
                    (
                        !request.Assigned
                        ||
                        task.IsAssigned == true
                    ) &&

                    // Locked filter
                    (
                        !request.Locked
                        ||
                        task.LockedDate != null
                    ) &&

                    // Overdue filter
                    (
                        !request.Overdue
                        ||
                        (
                            task.DueDate != null &&
                            task.DueDate < DateTime.Now
                        )
                    )

                select new InboxTaskDto
                {
                    Id = task.Id,

                    DocumentId = task.DocumentId,

                    DocumentTypeId = document.DocumentTypeBaseId,

                    DocumentType = documentType.Name,

                    ReferenceNumber = document.ReferenceNumber,

                    OwnerUserId = task.OwnerUserId,

                    OwnerUserName =
                        user.Firstname + " " + user.Lastname,

                    CreatedDate = task.CreatedDate,

                    OpenedDate = task.OpenedDate,

                    ClosedDate = task.ClosedDate,

                    DueDate = task.DueDate,

                    StatusId = task.StatusId,

                    StatusName = status.Name,

                    IsAssigned = task.IsAssigned,

                    IsTransferred = task.IsTransferred,

                    IsLocked = task.LockedDate != null,

                    IsOverdue =
                        task.DueDate != null &&
                        task.DueDate < DateTime.Now,

                    Instruction = task.Instruction,

                    ReplyInstruction = task.ReplyInstruction
                };

            var totalRecords = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.CreatedDate)
                .Skip(request.Start)
                .Take(request.Length)
                .ToListAsync();

            return new DataTableResponse<InboxTaskDto>
            {
                Draw = request.Draw,

                RecordsTotal = totalRecords,

                RecordsFiltered = totalRecords,

                Data = data
            };
        }

    }
}