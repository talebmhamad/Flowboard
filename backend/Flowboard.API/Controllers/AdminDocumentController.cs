using Flowboard.Intalio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Flowboard.API.Controllers
{
    [ApiController]
    [Route("api/admin/document")]
    public class AdminDocumentController : ControllerBase
    {
        private readonly IDocumentIntalioService _service;

        public AdminDocumentController(
            IDocumentIntalioService service
        )
        {
            _service = service;
        }

        [HttpGet("by-task/{taskId}")]
        public async Task<IActionResult> GetTrackingByTask(int taskId)
        {
            var result =
                await _service.GetDocumentByTaskIdAsync(taskId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}