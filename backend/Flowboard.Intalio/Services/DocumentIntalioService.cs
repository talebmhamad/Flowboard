using Flowboard.Application.DTOs.Document;
using Flowboard.Intalio.Interfaces;

namespace Flowboard.Intalio.Services
{
    public class DocumentIntalioService : IDocumentIntalioService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentIntalioService(
            IDocumentRepository documentRepository
        )
        {
            _documentRepository = documentRepository;
        }

        public async Task<DocumentDetailsDto> GetDocumentByTaskIdAsync(
            int taskId
        )
        {
            return await _documentRepository.GetDocumentByTaskIdAsync(taskId);
        }
    }
}