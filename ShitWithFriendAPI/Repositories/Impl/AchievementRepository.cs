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
    }
}
