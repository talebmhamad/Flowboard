using Flowboard.Application.DTOs;
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

    [HttpPost("active")]
    public async Task<IActionResult> GetActive([FromBody] TaskRequestDto request)
    {
        var result = await _service.GetActiveTasks(request);
        return Ok(result);
    }

    [HttpPost("completed")]
    public async Task<IActionResult> GetCompleted([FromBody] TaskRequestDto  request)
    {
        var result = await _service.GetCompletedTasks(request);
        return Ok(result);
    }

    [HttpPost("draft")]
    public async Task<IActionResult> GetDraft([FromBody] TaskRequestDto request)
    {
        var result = await _service.GetDraftTasks(request);
        return Ok(result);
    }

    [HttpGet("details/{taskId}")]
    public async Task<IActionResult> GetTaskDetails(int taskId)
    {
        await _service.ViewTaskAsync(taskId);

        var result = await _service.GetTaskDetails(taskId);

        return Ok(result);
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveTask([FromForm] SaveTaskDto request)
    {
        await _service.LockTaskAsync(int.Parse(request.Id));

        var result = await _service.SaveTaskAsync(request);

        await _service.UnlockTaskAsync(int.Parse(request.Id));

        return Ok(result);
    }

    [HttpPost("saveandsend")]
    public async Task<IActionResult> SaveAndSendTask([FromForm] SaveTaskDto request)
    {
        await _service.LockTaskAsync(int.Parse(request.Id));

        var result = await _service.SaveAndSendTaskAsync(request);

        await _service.UnlockTaskAsync(int.Parse(request.Id));

        return Ok(result);
    }

}