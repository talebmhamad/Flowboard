using Flowboard.Application.DTOs.Document;
using Flowboard.Intalio.Interfaces;
using Intalio.Case.Portal.Core.DAL;
using Microsoft.EntityFrameworkCore;

namespace Flowboard.Intalio.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly CasePortalContext _db;

        public DocumentRepository(CasePortalContext db)
        {
            _db = db;
        }

        public async Task<DocumentDetailsDto>GetDocumentByTaskIdAsync(long taskId)
        {
            var DocumentDetailsDto = new DocumentDetailsDto();
            var result =
                await
                (
                    from task in _db.Task

                    join document in _db.Document
                        on task.DocumentId equals document.Id

                    join documentPortal in _db.DocumentPortal
                        on document.Id equals documentPortal.Id

                    join workflowInstance in _db.WorkflowInstances
                        on document.WorkflowInstanceId
                            equals workflowInstance.WorkflowInstanceId

                    join workflowDefinition in _db.WorkflowDefinition
                        on workflowInstance.WorkflowDefinitionId
                            equals workflowDefinition.WorkflowId

                    where task.Id == taskId

                    select new DocumentDetailsDto
                    {
                        Id = document.Id,

                        DocumentTypeId =
                            document.DocumentTypeBaseId,

                        DocumentTypeName = null,

                        FormData =
                            documentPortal.Form,

                        FormDesigner =
                            workflowDefinition.FormInputDesigner,

                        FormDesignerTranslation =
                            workflowDefinition.FormInputDesignerTranslation,

                        IsEnableEdit =
                            task.ClosedDate == null,

                        RowVersion =
                            document.RowVersion.ToString(),

                        Status =
                            document.StatusId
                    }
                )
                .FirstOrDefaultAsync();

            return result;
        }
    }
}