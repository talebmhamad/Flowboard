using Flowboard.Application.DTOs.Document;

namespace Flowboard.Intalio.Interfaces
{
    public interface IDocumentIntalioService
    {
        Task<DocumentDetailsDto> GetDocumentByTaskIdAsync(int taskId);
    }
}