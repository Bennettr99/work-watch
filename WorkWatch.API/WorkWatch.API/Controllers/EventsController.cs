using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WorkWatch.API.Models;
using WorkWatch.Services;

namespace WorkWatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly DataAccessService _dataAccessService;

        public EventsController()
        {
            _dataAccessService = new DataAccessService("localhost");
        }

        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            Debug.WriteLine($"User post request with {JsonConvert.SerializeObject(user)}");
            var userId = await _dataAccessService.AddUser(user.Username, user.MachineName);
            return Ok(userId);
        }

        [HttpPost]
        [Route("applications")]
        public async Task<IActionResult> PostApplication([FromBody] Application application)
        {
            Debug.WriteLine($"Application post request with {JsonConvert.SerializeObject(application)}");
            var applicationId = await _dataAccessService.AddApplication(application.UserId, application.Name);
            return Ok(applicationId);
        }

        [HttpPost]
        [Route("inputs")]
        public async Task<IActionResult> PostInput([FromBody] Input input)
        {
            Debug.WriteLine($"Input post request with {JsonConvert.SerializeObject(input)}");
            var inputId = await _dataAccessService.AddInput(input.UserId, input.ApplicationId);
            return Ok(inputId);
        }

        [HttpPatch]
        [Route("inputs")]
        public async Task<IActionResult> PatchInput([FromBody] int inputId)
        {
            Debug.WriteLine($"Input patch request with {inputId}");
            await _dataAccessService.UpdateInput(inputId);
            return NoContent();
        }
    }
}