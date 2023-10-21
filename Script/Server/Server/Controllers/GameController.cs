using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("Game")]
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("Create/{gameKey}/{playerKey}")]
        public Task<ActionResult> CreateGame(string? gameKey, string? playerKey)
        {
            if()
        }
    }
}
