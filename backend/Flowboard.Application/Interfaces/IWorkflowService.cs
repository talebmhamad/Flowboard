using Flowboard.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowboard.Application.Interfaces
{


    public interface IWorkflowService
    {
        Task<List<WorkflowDto>> GetWorkflowsAsync();
        Task<string> GetWorkflowFormAsync(int documentTypeId);
    }
}