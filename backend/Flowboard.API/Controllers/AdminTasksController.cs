using Flowboard.Application.DTOs;
using Flowboard.Intalio.Interfaces;
using Flowboard.Intalio.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Flowboard.API.Controllers
{
    [ApiController]
    [Route("api/admin/tasks")]
    [Authorize(Roles = "Administrator")]
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
        public async Task<IActionResult> GetInboxTasks([FromBody] TaskRequestDto request)
        {
            // Get logged in user id from token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value?? User.FindFirst("sub")?.Value?? User.FindFirst("userid")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User id not found in token.");

            int managerId = int.Parse(userIdClaim);

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