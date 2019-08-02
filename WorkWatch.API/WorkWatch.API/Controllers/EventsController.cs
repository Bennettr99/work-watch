using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            Debug.WriteLine($"User post request with {JsonConvert.SerializeObject(user)}");
            return Ok(1);
        }

        [HttpPost]
        [Route("applications")]
        public async Task<IActionResult> PostApplication([FromBody] Application application)
        {
            Debug.WriteLine($"Application post request with {JsonConvert.SerializeObject(application)}");
            return Ok(2);
        }

        [HttpPost]
        [Route("inputs")]
        public async Task<IActionResult> PostInput([FromBody] Input input)
        {
            Debug.WriteLine($"Input post request with {JsonConvert.SerializeObject(input)}");
            return Ok(1);
        }

        [HttpPatch]
        [Route("inputs")]
        public async Task<IActionResult> PatchInput([FromBody] int inputId)
        {
            Debug.WriteLine($"Input patch request with {inputId}");
            return NoContent();
        }
    }
}