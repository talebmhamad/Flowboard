using Flowboard.Intalio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserExtensionController : ControllerBase
{
    private readonly IUserExtensionService _userService;

    public UserExtensionController(IUserExtensionService service)
    {
        _userService = service;
    }

    [HttpGet("GetByManager")]
    public async Task<IActionResult> GetByManager([FromQuery] int ManagerId)
    {
        var result = await _userService.GetEmployeesByManagerIdAsync(ManagerId);
        return Ok(result);
    }

   
  
}