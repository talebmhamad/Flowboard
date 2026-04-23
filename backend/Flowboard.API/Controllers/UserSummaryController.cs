using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowboard.Application.Interfaces;

namespace Flowboard.API.Controllers
{
    [ApiController]
    [Route("api/user/summary")]
    [Authorize(Roles = "User")]
    public class UserSummaryController : ControllerBase
    {
        private readonly IUserSummaryService _service;

        public UserSummaryController(IUserSummaryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSummary()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token))
                return Unauthorized("Missing token");

            var result = await _service.GetSummary(token);

            return Ok(result);
        }
    }
}