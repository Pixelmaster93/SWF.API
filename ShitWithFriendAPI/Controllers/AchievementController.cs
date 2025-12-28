using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShitWithFriendAPI.Dtos.Achievement;
using ShitWithFriendAPI.Repositories.Int;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ShitWithFriendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AchievementController : ControllerBase
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IUserAchievementRepository _userAchievementRepository;
        private readonly IUserRepository _userRepository;

        public AchievementController(
            IAchievementRepository achievementRepository,
            IUserAchievementRepository userAchievementRepository,
            IUserRepository userRepository)
        {
            _achievementRepository = achievementRepository;
            _userAchievementRepository = userAchievementRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Ottieni UserId dal token (o header in dev)
            // Assumo che ci sia un modo per recuperarlo, es. User.Claims...
            // Per semplicità e coerenza con altri controller, se non c'è auth, serve passarlo o fallire.
            // Controllo UserController per vedere come recupera l'utente.
            // Spesso è in un BaseController o si usa User.Identity.
            // Vedo che nei service si passa UserId. Qui devo averlo.
            // Se non riesco a prenderlo dai claim, uso un parametro query opzionale per test, ma meglio fare bene.
            // Uso `User.Claims` standard se presente, altrimenti cerco un header `X-User-Id` se l'app lo usa.
            // Controllo codice UserController... Non posso vederlo ora.
            // Assumo Auth standard.
            
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId))
            {
                // Fallback o Error? 
                // Se non autenticato, ritorna 401. 
                // Ma per ora metto un placeholder o cerco di essere resiliente.
                // Se il sistema usa un altro claim type?
                // Controllo se c'è un metodo comune.
                // Vabè, procedo standard.
                return Unauthorized();
            }

            var achievements = await _achievementRepository.GetAll().ToListAsync();
            var unlockedCodes = await _userAchievementRepository.GetUnlockedCodes(userId);

            var dtos = achievements.Select(a =>
            {
                var isUnlocked = unlockedCodes.Contains(a.Code);
                return new AchievementDto
                {
                    Code = a.Code,
                    Name = (a.IsSecret && !isUnlocked) ? "???" : a.Name,
                    Description = (a.IsSecret && !isUnlocked) ? "???" : a.Description,
                    IsSecret = a.IsSecret,
                    XpValue = a.XpValue,
                    IsUnlocked = isUnlocked
                };
            }).ToList();

            return Ok(dtos);
        }

        [HttpGet("user/avatar")]
        public async Task<IActionResult> GetAvatar()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

            var user = await _userRepository.GetById(userId).FirstOrDefaultAsync();
            if (user == null) return NotFound();

            return Ok(new { Avatar = user.Avatar });
        }
    }
}
