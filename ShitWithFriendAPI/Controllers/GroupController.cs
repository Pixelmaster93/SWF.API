using Microsoft.AspNetCore.Mvc;
using ShitWithFriendAPI.Dtos.Group;
using ShitWithFriendAPI.Services.Int;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

namespace ShitWithFriendAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GroupDto>> GetGroups([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10, [FromQuery] Guid? userId = null)
        {
            if (pageNumber < 0) pageNumber = 0;
            var groups = _groupService.GetGroups(pageNumber, pageSize, userId);
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public ActionResult<GroupDto> GetGroupById(Guid id)
        {
            var group = _groupService.GetGroupById(id);
            if (group == null) return NotFound();
            return Ok(group);
        }

        [HttpGet("name/{name}")]
        public ActionResult<GroupDto> GetGroupByName(string name)
        {
            var group = _groupService.GetGroupByName(name);
            if (group == null) return NotFound();
            return Ok(group);
        }

        [HttpPost]
        public ActionResult<GroupDto> CreateGroup([FromBody] CreateGroupRequestDto createGroupRequestDto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid? userId = null;
            if (Guid.TryParse(userIdString, out var parsedId))
            {
                userId = parsedId;
            }

            var group = _groupService.CreateGroup(createGroupRequestDto, userId);
            return CreatedAtAction(nameof(GetGroupById), new { id = group.Id }, group);
        }

        [HttpPut("{id}")]
        public ActionResult<GroupDto> UpdateGroup(Guid id, [FromBody] UpdateFroupRequest updateGroupRequestDto)
        {
            var group = _groupService.UpdateGroup(id, updateGroupRequestDto);
            if (group == null) return NotFound();
            return Ok(group);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(Guid id)
        {
            _groupService.DeleteGroup(id);
            return NoContent();
        }
    }
}
