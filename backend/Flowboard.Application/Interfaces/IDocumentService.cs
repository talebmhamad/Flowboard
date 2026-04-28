using Flowboard.Application.DTOs;
using System.Threading.Tasks;


public interface IDocumentService
{
    Task<string> SaveDocumentAsync(SaveDocumentDto request);
}