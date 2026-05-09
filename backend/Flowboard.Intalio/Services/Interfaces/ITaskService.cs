using Flowboard.Application.DTOs;

namespace Flowboard.Intalio.Services.Interfaces
{
    public interface ITaskService
    {
        Task<DataTableResponse<InboxTaskDto>> GetInboxTasksAsync(List<long> userIds, TaskRequestDto request);
    }
}