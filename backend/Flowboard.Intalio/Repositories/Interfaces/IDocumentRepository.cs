using Flowboard.Application.DTOs.Document;

namespace Flowboard.Intalio.Interfaces
{
    public interface IDocumentRepository
    {
        Task<DocumentDetailsDto> GetDocumentByTaskIdAsync(long taskId);
    }
}