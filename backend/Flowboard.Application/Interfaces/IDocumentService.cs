using Flowboard.Application.DTOs;
using System.Threading.Tasks;


public interface IDocumentService
{
    Task<string> SaveDocumentAsync(SaveDocumentDto request);

    Task<string> SaveAndSendDocumentAsync(SaveDocumentDto request);

    Task<string> GetDocumentBasicInfoByTaskId(int taskId);

    Task<string> GetDocumentByTaskId(int taskId);

}