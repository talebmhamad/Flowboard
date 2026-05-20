using Microsoft.AspNetCore.Mvc;
using Flowboard.Application.Interfaces;

namespace Flowboard.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserSummaryController : ControllerBase
    {
        private readonly IUserSummaryService _service;

        public UserSummaryController(IUserSummaryService service)
        {
            _service = service;
        }

        [HttpGet, Route("summary")]
        public async Task<IActionResult> GetSummary()
        {
            try
            {
                var result = await _service.GetSummary();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while fetching summary",
                    error = ex.Message
                });
            }
        }

    }
}