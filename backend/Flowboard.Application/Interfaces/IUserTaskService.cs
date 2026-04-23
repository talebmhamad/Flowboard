using System.Threading.Tasks;

namespace Flowboard.Application.Interfaces
{
    public interface IUserTaskService
    {
        Task<object> GetActiveTasks(string token);      
        Task<object> GetCompletedTasks(string token);  
        Task<object> GetDraftTasks(string token);      
        Task<object> GetMyRequests(string token);      
    }
}