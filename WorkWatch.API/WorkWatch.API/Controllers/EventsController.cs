using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkWatch.API.Models;

namespace WorkWatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            return Ok(1);
        }

        [HttpPost]
        [Route("applications")]
        public async Task<IActionResult> PostApplication([FromBody] Application application)
        {
            return Ok(2);
        }

        [HttpPost]
        [Route("inputs")]
        public async Task<IActionResult> PostInput([FromBody] Input input)
        {
            return Ok(1);
        }

        [HttpPatch]
        [Route("inputs")]
        public async Task<IActionResult> PatchInput(int inputId)
        {
            return NoContent();
        }
    }
}