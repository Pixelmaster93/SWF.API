using Microsoft.AspNetCore.Mvc;
using ShitWithFriendAPI.Dtos.Game;
using ShitWithFriendAPI.Services.Int;

namespace ShitWithFriendAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GameDto>> GetGames([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var games = _gameService.GetGames(pageNumber, pageSize);
            return Ok(games);
        }

        [HttpGet("{id}")]
        public ActionResult<GameDto> GetGameById(Guid id)
        {
            var game = _gameService.GetGameById(id);
            if (game == null) return NotFound();
            return Ok(game);
        }

        [HttpPost]
        public ActionResult<GameDto> CreateGame([FromBody] CreateGameRequestDto createGameRequestDto)
        {
            var game = _gameService.CreateGame(createGameRequestDto);
            return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
        }

        [HttpPut("{id}")]
        public ActionResult<GameDto> UpdateGame(Guid id, [FromBody] UpdateGameRequestDto updateGameRequestDto)
        {
            var game = _gameService.UpdateGame(id, updateGameRequestDto);
            if (game == null) return NotFound();
            return Ok(game);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGame(Guid id)
        {
            _gameService.DeleteGame(id);
            return NoContent();
        }
    }
}
