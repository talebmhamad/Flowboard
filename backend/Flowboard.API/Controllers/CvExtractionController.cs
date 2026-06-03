using Flowboard.Application.DTOs;
using Flowboard.Application.Interfaces.Flowboard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CvExtractionController : ControllerBase
{
    private readonly ICvExtractionService _cvExtractionService;

    public CvExtractionController(ICvExtractionService cvExtractionService)
    {
        _cvExtractionService = cvExtractionService;
    }

    [HttpPost("extract")]
    public async Task<IActionResult> Extract([FromBody] CvRequestDto request)
    {
        try
        {
            if (request == null || string.IsNullOrWhiteSpace(request.FileUrl))
                return BadRequest("FileUrl is required");

            var result = await _cvExtractionService.ExtractCvAsync(request.FileUrl);

            return Ok(new
            {
                success = true,
                data = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = ex.Message
            });
        }
    }
}