using Microsoft.AspNetCore.Mvc;
using ShitWithFriendAPI.Dtos.Poop;
using ShitWithFriendAPI.Services.Int;

namespace ShitWithFriendAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PoopController : ControllerBase
    {
        private readonly IPoopService _poopService;

        public PoopController(IPoopService poopService)
        {
            _poopService = poopService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PoopDto>> GetPoops([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var poops = _poopService.GetPoops(pageNumber, pageSize);
            return Ok(poops);
        }

        [HttpGet("filter")]
        public ActionResult<IEnumerable<PoopDto>> GetPoopsFromVariables([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] Guid? userId = null, [FromQuery] DateTime? dateFrom = null, [FromQuery] DateTime? dateTo = null)
        {
            var poops = _poopService.GetPoopsFromVariables(pageNumber, pageSize, userId, dateFrom, dateTo);
            return Ok(poops);
        }

        [HttpGet("{id}")]
        public ActionResult<PoopDto> GetPoopById(Guid id)
        {
            var poop = _poopService.GetPoopById(id);
            if (poop == null) return NotFound();
            return Ok(poop);
        }

        [HttpPost]
        public ActionResult<PoopDto> CreatePoop([FromBody] CreatePoopRequestDto createPoopRequestDto)
        {
            var poop = _poopService.CreatePoop(createPoopRequestDto);
            return CreatedAtAction(nameof(GetPoopById), new { id = poop.Id }, poop);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePoop(Guid id)
        {
            _poopService.DeletePoop(id);
            return NoContent();
        }
    }
}
