using GeoBlockAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GeoBlockAPI.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogsController : ControllerBase
    {
        private readonly InMemoryRepository _repo;

        public LogsController(InMemoryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("blocked-attempts")]
        public IActionResult GetBlockedAttempts()
        {
            return Ok(_repo.GetLogs());
        }
    }
}
