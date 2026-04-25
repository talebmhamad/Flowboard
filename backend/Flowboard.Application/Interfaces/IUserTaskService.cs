using System.Threading.Tasks;

namespace Flowboard.Application.Interfaces
{
    public interface IUserTaskService
    {
        Task<string> GetActiveTasks();
        Task<string> GetCompletedTasks();
        Task<string> GetDraftTasks();
        Task<string> GetMyRequests();
    }
}