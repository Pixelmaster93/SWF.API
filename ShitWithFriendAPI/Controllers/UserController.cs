using Microsoft.AspNetCore.Mvc;
using ShitWithFriendAPI.Dtos.User;
using ShitWithFriendAPI.Services.Int;
using ShitWithFriendAPI.Repositories.Int;
using Microsoft.AspNetCore.Authorization;

namespace ShitWithFriendAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserAchievementRepository _userAchievementRepository;

        public UserController(IUserService userService, IUserAchievementRepository userAchievementRepository)
        {
            _userService = userService;
            _userAchievementRepository = userAchievementRepository;
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
        [HttpPut("avatar")]
        public ActionResult UpdateAvatar([FromBody] UpdateAvatarRequestDto request)
        {
            // Ottieni UserId dal token
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                               ?? User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

            // Verifica ownership: è un achievement sbloccato O è di base?
            // Base avatars: POOP_1 ... POOP_? Assumiamo che POOP_1 sia sempre disponibile.
            // O controlliamo se inizia con "POOP_"?
            // User requirement: "Verifica che l'utente possieda quell'achievement (o che sia uno di base)."
            // Assumo che POOP_1 sia base. Vediamo se ci sono altri base.
            // Se Code inizia con "POOP_" e il numero è basso?
            // Meglio controllare se è sbloccato. POOP_1 è un achievement, quindi sarà in UserAchievements se sbloccato.
            // Ma POOP_1 è starter.
            // Controllo: se code è "POOP_1" ok. Altrimenti check repo.
            
            bool isBase = request.AvatarCode == "DEFAULT_1";
            bool hasUnlock = _userAchievementRepository.HasUnlock(userId, request.AvatarCode);

            if (!isBase && !hasUnlock)
            {
                return BadRequest("Avatar not unlocked.");
            }

            // Aggiorna user
            // Uso UserService? UpdateUserRequestDto richiede tutto.
            // Meglio fare un metodo ad hoc nel service o repository, o sporco qui per ora.
            // UserService.UpdateUser sovrascrive tutto.
            // Implementare un metodo UpdateAvatar in Service sarebbe meglio, ma per ora faccio quick fix se possibile.
            // UserService non ha UpdateAvatar.
            // Uso Repository o creo metodo in Service?
            // Modificare UserService è meglio.
            
            // Per ora delego a UserService creando un metodo ad-hoc per pulizia, ma non posso modificare UserService qui in parallelo.
            // Chiamo un metodo nuovo che creerò: _userService.UpdateAvatar(userId, code);
            
            _userService.UpdateAvatar(userId, request.AvatarCode);
            return Ok();
        }
    }
}
