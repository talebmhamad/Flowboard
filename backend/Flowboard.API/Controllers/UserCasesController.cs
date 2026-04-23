using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowboard.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User")]
public class UserCasesController : ControllerBase
{
    private readonly IUserCaseService _service;

    public UserCasesController(IUserCaseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyCases()
    {
        var userId = User.FindFirst("sub")?.Value;

        var result = await _service.GetMyCases(userId);

        return Ok(result);
    }

}