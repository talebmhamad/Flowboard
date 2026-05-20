using Flowboard.Application.DTOs;
using Flowboard.Intalio.Repositories;
using Flowboard.Intalio.Services.Interfaces;

namespace Flowboard.Intalio.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskRepository _taskRepository;

        public TaskService(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<DataTableResponse<InboxTaskDto>> GetInboxTasksAsync(List<long> userIds, TaskRequestDto request)
        {
            return await _taskRepository.GetInboxTasksAsync(userIds, request);
        }
    }
}