using HashStrike.Api.Models.Data;
using HashStrike.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HashStrike.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostsController : ControllerBase
    {
        private readonly HostRegitrationService _hostRegitrationService;
        private readonly TasksService _tasksService;
        private readonly ActivityCheckingService _activityCheckingService;
        private readonly ApplicationContext _db;
        public HostsController(ApplicationContext db)
        {
            _db = db;
            _hostRegitrationService = new HostRegitrationService(db);
            _tasksService = new TasksService(db);
            _activityCheckingService = new ActivityCheckingService(db);
        }

        [HttpGet("register")]
        public IActionResult HostRegister()
        {
            var hostName = _hostRegitrationService.CreateHostName();
            return Ok(hostName);
        }

        [HttpGet("{name}")]
        public IActionResult GetTaskForHost(string name)
        {
            var host = _db.Hosts.FirstOrDefault(h => h.Name == name);
            host.LastRequestTime = DateTime.Now;
            _db.SaveChanges();
            _tasksService.DistributeTasks();
            return Ok(host.Task);
         }

        [HttpGet("active-hosts")]
        public IActionResult GetActiveHosts()
        {
            var activeHosts = _activityCheckingService.GetActiveHosts(); 
            return Ok(activeHosts);
        }

        [HttpPost("{name}")]
        public IActionResult CreateAnswer(string name, [FromBody] string answer)
        {
            return _tasksService.CreateAnswer(name, answer);
        }

    }
}
