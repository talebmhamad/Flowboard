using Flowboard.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User")]
public class TasksController : ControllerBase
{
    private readonly IUserTaskService _service;

    public TasksController(IUserTaskService service)
    {
        _service = service;
    }

    private string GetToken()
    {
        return HttpContext.Request.Headers["Authorization"].ToString();
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var token = GetToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized();

        var result = await _service.GetActiveTasks(token);
        return Ok(result);
    }

    [HttpGet("completed")]
    public async Task<IActionResult> GetCompleted()
    {
        var token = GetToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized();

        var result = await _service.GetCompletedTasks(token);
        return Ok(result);
    }

    [HttpGet("draft")]
    public async Task<IActionResult> GetDraft()
    {
        var token = GetToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized();

        var result = await _service.GetDraftTasks(token);
        return Ok(result);
    }

    [HttpGet("myrequests")]
    public async Task<IActionResult> GetMyRequests()
    {
        var token = GetToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized();

        var result = await _service.GetMyRequests(token);
        return Ok(result);
    }
}