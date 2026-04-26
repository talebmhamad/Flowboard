using Microsoft.AspNetCore.Mvc;
using Flowboard.Application.Interfaces;

namespace Flowboard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkflowController : ControllerBase
{
    private readonly IWorkflowService _workflowService;

    public WorkflowController(IWorkflowService workflowService)
    {
        _workflowService = workflowService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkflows()
    {
        var workflows = await _workflowService.GetWorkflowsAsync();
        return Ok(workflows);
    }

    [HttpGet("form/{documentTypeId}")]
    public async Task<IActionResult> GetForm(int documentTypeId)
    {
        var form = await _workflowService.GetWorkflowFormAsync(documentTypeId);
        return Ok(form);
    }
}