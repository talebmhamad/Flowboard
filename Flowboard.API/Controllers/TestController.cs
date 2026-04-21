using Microsoft.AspNetCore.Mvc;

namespace Flowboard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Backend is working 🚀");
        }
    }
}