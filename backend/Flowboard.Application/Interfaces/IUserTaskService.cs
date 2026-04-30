using Flowboard.Application.DTOs;
using System.Threading.Tasks;

namespace Flowboard.Application.Interfaces
{
    public interface IUserTaskService
    {
        Task<string> GetActiveTasks(TaskInboxRequestDto request);
        Task<string> GetCompletedTasks();
        Task<string> GetDraftTasks();
        Task<string> GetMyRequests();
    }
}