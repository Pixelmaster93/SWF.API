using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;

namespace ShitWithFriendAPI.Repositories.Impl
{
    public class AchievementRepository : IAchievementRepository
    {
        protected readonly SWFContext _context;

        public AchievementRepository(SWFContext context)
        {
            _context = context;
        }

        public IQueryable<Achievement> GetAll() => _context.Achievements;

        public async Task<Achievement?> GetByCodeAsync(string code)
        {
            return await _context.Achievements.FindAsync(code);
        }

        public async Task AddAsync(Achievement achievement)
        {
            await _context.Achievements.AddAsync(achievement);
        }

        public Task UpdateAsync(Achievement achievement)
        {
            _context.Achievements.Update(achievement);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
