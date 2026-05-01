using Flowboard.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Flowboard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _service;
        public DocumentController(IDocumentService service)
        {
            _service = service;
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromForm] SaveDocumentDto request)
        {
            var result = await _service.SaveDocumentAsync(request);
            return Ok(result);
        }

        [HttpPost("saveandsend")]
        public async Task<IActionResult> SaveAndSend([FromForm] SaveDocumentDto request)
        {
            var result = await _service.SaveAndSendDocumentAsync(request);
            return Ok(result);
        }

        [HttpGet("basic-info/{taskId}")]
        public async Task<IActionResult> GetBasicInfo(int taskId)
        {
            var result = await _service.GetDocumentBasicInfoByTaskId(taskId);
            return Ok(result);
        }

        [HttpGet("by-task/{taskId}")]
        public async Task<IActionResult> GetByTaskId(int taskId)
        {
            var result = await _service.GetDocumentByTaskId(taskId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetDocumentById(id);
            return Ok(result);
        }
    }
}
