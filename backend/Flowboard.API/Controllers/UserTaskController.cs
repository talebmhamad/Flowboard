using Flowboard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IUserTaskService _service;

    public TasksController(IUserTaskService service)
    {
        _service = service;
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var result = await _service.GetActiveTasks();
        return Ok(result);
    }

    [HttpGet("completed")]
    public async Task<IActionResult> GetCompleted()
    {
        var result = await _service.GetCompletedTasks();
        return Ok(result);
    }

    [HttpGet("draft")]
    public async Task<IActionResult> GetDraft()
    {
        var result = await _service.GetDraftTasks();
        return Ok(result);
    }

    [HttpGet("myrequests")]
    public async Task<IActionResult> GetMyRequests()
    {
        var result = await _service.GetMyRequests();
        return Ok(result);
    }
}