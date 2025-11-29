using Microsoft.AspNetCore.Mvc;
using ShitWithFriendAPI.Dtos.User;
using ShitWithFriendAPI.Services.Int;

namespace ShitWithFriendAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var users = _userService.GetUsers(pageNumber, pageSize);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUserById(Guid id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("username/{username}")]
        public ActionResult<UserDto> GetUserByUsername(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserDto> CreateUser([FromBody] CreateUserRequestDto createUserRequestDto)
        {
            var user = _userService.CreateUser(createUserRequestDto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public ActionResult<UserDto> UpdateUser(Guid id, [FromBody] UpdateUserRequestDto updateUserRequestDto)
        {
            var user = _userService.UpdateUser(id, updateUserRequestDto);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
