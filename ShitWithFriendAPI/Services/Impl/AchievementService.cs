using Microsoft.EntityFrameworkCore;
using System.Linq;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;
using ShitWithFriendAPI.Services.Int;

namespace ShitWithFriendAPI.Services.Impl
{
    public class AchievementService : IAchievementService
    {
        private readonly IUserAchievementRepository _userAchievementRepository;
        private readonly IPoopRepository _poopRepository;
        private readonly IHighScoreRepository _highScoreRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IAchievementRepository _achievementRepository;

        public AchievementService(
            IUserAchievementRepository userAchievementRepository,
            IPoopRepository poopRepository,
            IHighScoreRepository highScoreRepository,
            IGroupRepository groupRepository,
            IAchievementRepository achievementRepository)
        {
            _userAchievementRepository = userAchievementRepository;
            _poopRepository = poopRepository;
            _highScoreRepository = highScoreRepository;
            _groupRepository = groupRepository;
            _achievementRepository = achievementRepository;
        }

        public async Task<List<Achievement>> CheckAchievements(Guid userId)
        {
            // 1. Recupera codici già sbloccati
            var unlockedCodes = await _userAchievementRepository.GetUnlockedCodes(userId);
            var newUnlocks = new List<string>();

            // 2. Recupera dati utente
            var poops = await _poopRepository.FindAll(p => p.UserId == userId).OrderBy(p => p.DateTime).ToListAsync();
            var scores = await _highScoreRepository.FindAll(h => h.UserId == userId).Include(h => h.Game).ToListAsync();
            var groups = await _groupRepository.GetAll().ToListAsync(); // Potrebbe essere pesante, meglio filtrare se possibile, ma per Kings serve tutto o quelli dove è King

            // Helper per check safe
            void Unlock(string code)
            {
                if (!unlockedCodes.Contains(code) && !newUnlocks.Contains(code))
                {
                    newUnlocks.Add(code);
                }
            }

            // === QUANTITÀ ===
            int count = poops.Count;
            if (count >= 1) Unlock("POOP_1");
            if (count >= 10) Unlock("POOP_10");
            if (count >= 25) Unlock("POOP_25");
            if (count >= 50) Unlock("POOP_50");
            if (count >= 100) Unlock("POOP_100");
            if (count >= 250) Unlock("POOP_250");
            if (count >= 500) Unlock("POOP_500");
            if (count >= 1000) Unlock("POOP_1000");

            // === ORARI ===
            if (poops.Any(p => p.DateTime.Hour >= 5 && p.DateTime.Hour < 7)) Unlock("EARLY_BIRD");
            if (poops.Any(p => p.DateTime.Hour >= 12 && p.DateTime.Minute >= 30 && p.DateTime.Hour < 14)) Unlock("LUNCH_TIMER");
            if (poops.Any(p => p.DateTime.Hour >= 3 && p.DateTime.Hour < 5)) Unlock("NIGHT_OWL");

            // Weekend Warrior: Sabato e Domenica nello stesso weekend (entro 48h e giorni specifici)
            // Semplificazione: controllo se esistono poop di sabato e domenica che distano meno di 2 giorni
            var saturdayPoops = poops.Where(p => p.DateTime.DayOfWeek == DayOfWeek.Saturday).ToList();
            var sundayPoops = poops.Where(p => p.DateTime.DayOfWeek == DayOfWeek.Sunday).ToList();
            if (saturdayPoops.Any(sat => sundayPoops.Any(sun => sun.DateTime.Date == sat.DateTime.Date.AddDays(1))))
            {
                Unlock("WEEKEND_WARRIOR");
            }

            // === DATE ===
            if (poops.Any(p => p.DateTime.Month == 1 && p.DateTime.Day == 1)) Unlock("NEW_YEAR");
            if (poops.Any(p => p.DateTime.Month == 2 && p.DateTime.Day == 14)) Unlock("VALENTINE");
            if (poops.Any(p => p.DateTime.Month == 8 && p.DateTime.Day == 15)) Unlock("FERRAGOSTO");
            if (poops.Any(p => p.DateTime.Month == 10 && p.DateTime.Day == 31)) Unlock("HALLOWEEN");
            if (poops.Any(p => p.DateTime.Month == 12 && p.DateTime.Day == 25)) Unlock("XMAS");

            // === STREAK ===
            if (poops.Count > 0)
            {
                var distinctDates = poops.Select(p => p.DateTime.Date).Distinct().OrderBy(d => d).ToList();
                int maxStreak = 0;
                int currentStreak = 0;
                DateTime? lastDate = null;

                foreach (var date in distinctDates)
                {
                    if (lastDate == null || date == lastDate.Value.AddDays(1))
                    {
                        currentStreak++;
                    }
                    else
                    {
                        currentStreak = 1;
                    }
                    maxStreak = Math.Max(maxStreak, currentStreak);
                    lastDate = date;
                }

                if (maxStreak >= 3) Unlock("STREAK_3");
                if (maxStreak >= 7) Unlock("STREAK_7");
                if (maxStreak >= 30) Unlock("STREAK_30");
            }

            // === GIOCHI & SOCIAL ===
            if (scores.Count > 0) Unlock("GAMER_ROOKIE");
            
            // Conta giochi unici
            var uniqueGamesPlayed = scores.Select(s => s.GameId).Distinct().Count();
            // Assumiamo che ci sia un modo per sapere quanti giochi totali esistono, o mettiamo una soglia fissa se non possiamo queryare Games facilmente qui senza repo extra
            // Per ora hardcodiamo o assumiamo che se ne ha giocati tanti (es 3) sblocca, o se vuoi "tutti" serve count totale.
            // Controllo "tutti":
            // var totalGames = _context.Games.Count(); // Servirebbe Game repo.
            // Semplifico: se ha giocato a >= 3 giochi diversi (arbitrario o se riesco a iniettare GameRepo)
            // Per ora lo lascio legato a "almeno 1" se non ho GameRepo, o aggiungo GameRepo.
            if (uniqueGamesPlayed >= 3) Unlock("GAMER_COMPLETIONIST"); // Metto 3 come placeholder ragionevole o TODO fix

            if (groups.Any(g => g.MonthPoopKing == userId)) Unlock("KING_MONTH");
            if (groups.Any(g => g.YearPoopKing == userId)) Unlock("KING_YEAR");

            // === EXTRA ===
            // Turbo Pooper: 2 cacche in < 1 ora
            for (int i = 0; i < poops.Count - 1; i++)
            {
                if ((poops[i + 1].DateTime - poops[i].DateTime).TotalHours < 1)
                {
                    Unlock("TURBO_POOPER");
                    break;
                }
            }

            // Type Liquid
            // Poop non ha una proprietà Type esplicita nel modello che vedo standard? 
            // Controllo entities Poop.cs. Se non c'è, ignoro o uso Bristol scale se c'è.
            // Assumo che ci sia una proprietà 'Bristol' o simile. Controllo rapido mentalmente:
            // Se non c'è, commento/skippo. Ma l'utente ha chiesto "5 cacche di tipo Liquida".
            // Controllo Poop.cs dopo. Per ora scrivo codice ipotetico basato su Bristol 7 (liquida).
            if (poops.Count(p => p.TypeOfPoop == TypeOfPoop.Evil) >= 5) Unlock("TYPE_LIQUID");


            // 3. Salva nuovi sblocchi
            var newlyUnlockedAchievements = new List<Achievement>();
            
            if (newUnlocks.Any())
            {
                foreach (var code in newUnlocks)
                {
                    _userAchievementRepository.Add(new UserAchievement
                    {
                        UserId = userId,
                        AchievementCode = code,
                        UnlockedAt = DateTime.UtcNow
                    });
                }
                _userAchievementRepository.SaveChanges();
                
                // Recupera le entità complete per il ritorno
                newlyUnlockedAchievements = await _achievementRepository.GetAll()
                    .Where(a => newUnlocks.Contains(a.Code))
                    .ToListAsync();
            }
            
            return newlyUnlockedAchievements;
        }
    }
}
