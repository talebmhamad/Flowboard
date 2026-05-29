using Flowboard.Application.DTOs;


namespace Flowboard.Application.Interfaces
{


    public interface IWorkflowService
    {
        Task<List<WorkflowDto>> GetWorkflowsAsync();
        Task<string> GetWorkflowFormAsync(int documentTypeId);
    }
}