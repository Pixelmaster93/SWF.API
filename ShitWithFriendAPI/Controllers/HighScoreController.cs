using Microsoft.AspNetCore.Mvc;
using ShitWithFriendAPI.Dtos.HighScore;
using ShitWithFriendAPI.Services.Int;

namespace ShitWithFriendAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HighScoreController : ControllerBase
    {
        private readonly IHighScoreService _highScoreService;

        public HighScoreController(IHighScoreService highScoreService)
        {
            _highScoreService = highScoreService;
        }

        [HttpGet("game/{gameId}")]
        public ActionResult<IEnumerable<HighScoreDto>> GetGameHighScores(Guid gameId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var highScores = _highScoreService.GetGameHighScores(pageNumber, pageSize, gameId);
            return Ok(highScores);
        }

        [HttpGet("game/{gameId}/user/{userId}")]
        public ActionResult<IEnumerable<HighScoreDto>> GetGameHighScoresFromUser(Guid gameId, Guid userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var highScores = _highScoreService.GetGameHighScoresFromUser(pageNumber, pageSize, gameId, userId);
            return Ok(highScores);
        }

        [HttpGet("{id}")]
        public ActionResult<HighScoreDto> GetHighScoreById(Guid id)
        {
            var highScore = _highScoreService.GetHighScoreById(id);
            if (highScore == null) return NotFound();
            return Ok(highScore);
        }

        [HttpPost]
        public ActionResult<HighScoreDto> CreateHighScore([FromBody] CreateHighScoreRequestDto createHighScoreRequestDto)
        {
            var highScore = _highScoreService.CreateHighScore(createHighScoreRequestDto);
            return CreatedAtAction(nameof(GetHighScoreById), new { id = highScore.Id }, highScore);
        }

        [HttpPut("{id}")]
        public ActionResult<HighScoreDto> UpdateHighScore(Guid id, [FromBody] UpdateHighScoreRequestDto updateHighScoreRequestDto)
        {
            var highScore = _highScoreService.UpdateHighScore(id, updateHighScoreRequestDto);
            if (highScore == null) return NotFound();
            return Ok(highScore);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHighScore(Guid id)
        {
            _highScoreService.DeleteHighScore(id);
            return NoContent();
        }
    }
}
