using Flowboard.Application.DTOs;
using Flowboard.Intalio.Interfaces;
using Flowboard.Intalio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Flowboard.API.Controllers
{
    [ApiController]
    [Route("api/admin/tasks")]
    public class AdminTasksController : ControllerBase
    {
        private readonly IUserExtensionService _userService;
        private readonly ITaskService _taskService;

        public AdminTasksController(IUserExtensionService userService,ITaskService taskService)
        {
            _userService = userService;
            _taskService = taskService;
        }

        [HttpPost("inbox")]
        public async Task<IActionResult> GetInboxTasks([FromQuery] int managerId,[FromBody] TaskRequestDto request){
            // Get employees by manager
            var employees =
                await _userService.GetEmployeesByManagerIdAsync(managerId);

            // Extract user ids
            var userIds = employees
                .Select(x => (long)x.Id)
                .ToList();

            // Get inbox tasks
            var result =
                await _taskService.GetInboxTasksAsync(userIds, request);

            return Ok(result);
        }
    }
}