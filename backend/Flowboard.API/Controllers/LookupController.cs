using Flowboard.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flowboard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("GetLookupItemsByName")]
        public async Task<IActionResult> GetLookupItemsByName(
            string name,
            int language
        )
        {
            var result = await _lookupService
                .GetLookupItemsByNameAsync(name, language);

            return Ok(result);
        }
    }
}