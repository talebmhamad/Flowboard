using Flowboard.Application.DTOs;
using System.Threading.Tasks;

namespace Flowboard.Application.Interfaces
{
    public interface IUserTaskService
    {
        Task<string> GetActiveTasks(TaskRequestDto request);
        Task<string> GetCompletedTasks(TaskRequestDto request);
        Task<string> GetDraftTasks(TaskRequestDto request);
        Task<string> GetTaskDetails(int taskId);
        Task<string> ViewTaskAsync(int taskId);  
        Task<string> LockTaskAsync(int taskId);   
        Task<string> UnlockTaskAsync(int taskId); 
        Task<string> SaveTaskAsync(SaveTaskDto request);
        Task<string> SaveAndSendTaskAsync(SaveTaskDto request);
    }
}